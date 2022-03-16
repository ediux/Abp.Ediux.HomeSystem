using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

using Ediux.HomeSystem.Blazor.Menus;
using Ediux.HomeSystem.BlobContainers;
using Ediux.HomeSystem.EntityFrameworkCore;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Options;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Serilog;

using System;
using System.IO;
using System.Linq;
using System.Net.Http;

using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Components.Server.BasicTheme;
using Volo.Abp.AspNetCore.Components.Server.BasicTheme.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Data;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Identity.Blazor.Server;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Blazor.Server;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Blazor.Server;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.MediaDescriptors;

namespace Ediux.HomeSystem.Blazor
{
    [DependsOn(
        typeof(HomeSystemApplicationModule),
        typeof(HomeSystemEntityFrameworkCoreModule),
        typeof(HomeSystemHttpApiModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAutofacModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreComponentsServerBasicThemeModule),
        typeof(AbpIdentityBlazorServerModule),
        typeof(AbpTenantManagementBlazorServerModule),
        typeof(AbpSettingManagementBlazorServerModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpBlobStoringFileSystemModule)
       )]
    public class HomeSystemBlazorModule : AbpModule
    {

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            context.Services.PreConfigure<PluginsOption>(option =>
            {
                if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
                {
                    option.RootPath = Environment.GetEnvironmentVariable("PLUGINS_HOME");
                }
                else
                {
                    option.RootPath = Path.Combine(hostingEnvironment.ContentRootPath, "host");
                }
            });

            context.Services.Replace(ServiceDescriptor.Transient<IConnectionStringResolver, AddInsDbContextConnectionStringResolver>());

            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(HomeSystemResource),
                    typeof(HomeSystemDomainModule).Assembly,
                    typeof(HomeSystemDomainSharedModule).Assembly,
                    typeof(HomeSystemApplicationModule).Assembly,
                    typeof(HomeSystemApplicationContractsModule).Assembly,
                    typeof(HomeSystemBlazorModule).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureDbConnectionStrings(context);
            ConfigureUrls(configuration);
            ConfigureBundles();
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureSwaggerServices(context.Services);
            ConfigureBlob(context);
            ConfigureAutoApiControllers();
            ConfigureHttpClient(context);
            ConfigureBlazorise(context);
            ConfigureRouter(context);
            ConfigureMenu(context);

            GlobalFeatureManager.Instance.Modules.CmsKit(cmsKit =>
            {
                cmsKit.EnableAll();
            });
        }

        private void ConfigureDbConnectionStrings(ServiceConfigurationContext context)
        {
            context.Services.Configure<AbpDbConnectionOptions>(options =>
            {
                if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
                {
                    string connFromOS = HomeSystemConsts.GetDefultConnectionStringFromOSENV();
#if DEBUG
                    Log.Logger.Information($"ConnectionStrings.Default:{options.ConnectionStrings.Default}");
#endif
                    string[] connKeys = options.ConnectionStrings.Keys
                    .Distinct()
                    .ToArray();

                    if (connKeys.Any())
                    {
                        foreach (string key in connKeys)
                        {
#if DEBUG
                            Log.Logger.Information($"Connection String [{key}] : {options.ConnectionStrings[key]} => {connFromOS}.");
#endif
                            options.ConnectionStrings[key] = connFromOS;
                        }
                    }
                }
            });
        }
        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureBundles()
        {
            Configure<AbpBundlingOptions>(options =>
            {
                // MVC UI
                options.StyleBundles.Configure(
                    BasicThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/global-styles.css");
                    }
                );

                //BLAZOR UI
                options.StyleBundles.Configure(
                    BlazorBasicThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/blazor-global-styles.css");
                        //You can remove the following line if you don't use Blazor CSS isolation for components
                        bundle.AddFiles("/Ediux.HomeSystem.Blazor.styles.css");
                    }
                );
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "HomeSystem";
                });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Ediux.HomeSystem.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Ediux.HomeSystem.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Ediux.HomeSystem.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Ediux.HomeSystem.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemBlazorModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
                options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
                options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
                options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
                options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
                options.Languages.Add(new LanguageInfo("it", "it", "Italian", "it"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("jp", "jp", "日本語"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
                options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
                options.Languages.Add(new LanguageInfo("es", "es", "Español"));
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddAbpSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeSystem API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }

        private static void ConfigureHttpClient(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri("/")
            });
        }

        private void ConfigureBlazorise(ServiceConfigurationContext context)
        {
            context.Services
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
        }

        private void ConfigureMenu(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new HomeSystemMenuContributor());
            });

            
        }

        private void ConfigureRouter(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(HomeSystemBlazorModule).Assembly;
            });
        }
        private void ConfigureBlob(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.Configure<MediaContainer>(option =>
                {
                    option.UseFileSystem(fs =>
                    {
                        fs.BasePath = hostingEnvironment.ContentRootPath;
                    });
                });

                options.Containers.Configure<PluginsContainer>(option =>
                {
                    option.UseFileSystem(fs =>
                    {
                        fs.BasePath = hostingEnvironment.ContentRootPath;
                    });
                });

                options.Containers.ConfigureDefault(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = hostingEnvironment.ContentRootPath;
                    });
                });


            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(HomeSystemApplicationModule).Assembly);
            });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HomeSystemBlazorModule>();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();

            app.UseAbpRequestLocalization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (Environment.GetEnvironmentVariable("AbpMultiTenancy") == "Enabled")
            {
                app.UseMultiTenancy();
            }


            app.UseUnitOfWork();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeSystem API");
            });
            app.UseConfiguredEndpoints();
        }
    }
}

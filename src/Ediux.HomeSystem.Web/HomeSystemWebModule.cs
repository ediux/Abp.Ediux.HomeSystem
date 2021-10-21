using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ediux.HomeSystem.EntityFrameworkCore;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.MultiTenancy;
using Ediux.HomeSystem.Web.Menus;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Modularity.PlugIns;
using Volo.Docs;
using Volo.Docs.Admin;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;
using Ediux.HomeSystem.Web.Settings;
using Microsoft.AspNetCore.Authorization;

namespace Ediux.HomeSystem.Web
{
    [DependsOn(
        typeof(DocsWebModule),
        typeof(HomeSystemHttpApiModule),
        typeof(HomeSystemApplicationModule),
        typeof(HomeSystemEntityFrameworkCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpSettingManagementWebModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule)
        )]
    [DependsOn(typeof(DocsAdminWebModule))]
    public class HomeSystemWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(HomeSystemResource),
                    typeof(HomeSystemDomainModule).Assembly,
                    typeof(HomeSystemDomainSharedModule).Assembly,
                    typeof(HomeSystemApplicationModule).Assembly,
                    typeof(HomeSystemApplicationContractsModule).Assembly,
                    typeof(HomeSystemWebModule).Assembly
                );
            });



        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            var env = hostingEnvironment;

            ConfigureUrls(configuration);
            ConfigureBundles();
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = hostingEnvironment.ContentRootPath;
                    });
                });
            });

            Configure<SettingManagementPageOptions>(options =>
            {
                options.Contributors.Add(new WebSiteSettingPageContributor(context.Services.GetRequiredService<IAuthorizationService>()));
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
                options.StyleBundles.Configure(
                    BasicThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/global-styles.css");
                    }
                );

                options.StyleBundles.Configure(
                    HomeSystemBundles.Styles.jqDT_BS4,
                    bundle =>
                    {
                        bundle.AddFiles("/libs/datatables.net-bs4/css/dataTables.bootstrap4.css");
                    });

                options.ScriptBundles.Configure(
                    HomeSystemBundles.Scripts.jqDT,
                    bundle =>
                    {
                        bundle.AddFiles(
                            "/libs/datatables.net/js/jquery.dataTables.js",
                            "/libs/select2/js/select2.full.min.js");
                    });

                options.ScriptBundles.Configure(
                    HomeSystemBundles.Scripts.jqDT_BS4,
                    bundle =>
                    {
                        bundle.AddFiles(
                            "/libs/datatables.net/js/jquery.dataTables.js",
                            "/libs/datatables.net-bs4/js/dataTables.bootstrap4.js",
                            "/libs/select2/js/select2.full.min.js");
                    });

                options.ScriptBundles.Configure(
                    HomeSystemBundles.Scripts.DataGrid,
                    bundle =>
                    {
                        bundle.AddFiles(
                            "/libs/datatables.net/js/jquery.dataTables.js",
                            "/libs/datatables.net-bs4/js/dataTables.bootstrap4.js",
                            "/custlibs/datagrid/datatables/datatables.bundle.js",
                            "/libs/select2/js/select2.full.min.js",
                            "/custlibs/datagrid/datagrid.js");
                    });

                options.ScriptBundles
                    .Configure(typeof(IndexModel).FullName,
                        configuration =>
                        {
                            configuration.AddFiles("/Components/WebSettingsGroup/Default.js");
                        });

                options.ScriptBundles
                    .Configure(typeof(Pages.PasswordBook.IndexModel).FullName,
                        configuration =>
                        {
                            configuration.AddFiles("/Pages/PasswordBook/Index.js");
                        });

                options.ScriptBundles
                   .Configure(typeof(Pages.MIMETypeManager.IndexModel).FullName,
                       configuration =>
                       {
                           configuration.AddFiles("/Pages/MIMETypeManager/Index.js");
                       });

                options.ScriptBundles
                   .Configure("fullcalendar",
                       configuration =>
                       {
                           configuration.AddFiles("/custlibs/fullcalendar/main.js", "/custlibs/fullcalendar/locales-all.min.js");
                       });

                options.StyleBundles
                    .Configure("fullcalendar",
                        configuration =>
                        {
                            configuration.AddFiles("/custlibs/fullcalendar/main.css");
                        });

                options.ScriptBundles
                    .Configure(typeof(Pages.PersonalCalendar.IndexModel).FullName,
                        configuration =>
                        {
                            configuration.AddFiles("/Pages/PersonalCalendar/Index.js");
                        });

                options.StyleBundles
                    .Configure("toastui.editor",
                        configuration =>
                        {
                            configuration.AddFiles("/libs/tui-editor/toastui-editor.css");
                        });

                options.ScriptBundles
                  .Configure("toastui.editor",
                      configuration =>
                      {
                          configuration.AddFiles("/libs/tui-editor/toastui-editor.js");
                      });

                options.ScriptBundles
                   .Configure("ckeditor5",
                       configuration =>
                       {
                           configuration.AddFiles("/custlibs/ckeditor/ckeditor.js");
                           configuration.AddFiles("/custlibs/ckeditor/translations/ja.js",
                               "/custlibs/ckeditor/translations/zh.js");
                       });

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

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HomeSystemWebModule>();
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
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemWebModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("jp", "jp", "日本語"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureNavigationServices()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new HomeSystemMenuContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(HomeSystemApplicationModule).Assembly);
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

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            //if (MultiTenancyConsts.IsEnabled)
            //{
            //    app.UseMultiTenancy();
            //}

            app.UseUnitOfWork();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeSystem API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}

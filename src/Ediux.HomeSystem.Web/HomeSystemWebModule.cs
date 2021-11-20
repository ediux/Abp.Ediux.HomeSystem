using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ediux.HomeSystem.EntityFrameworkCore;
using Ediux.HomeSystem.Localization;
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
using Volo.Abp.Localization.ExceptionHandling;
using Volo.CmsKit.Admin;
using Ediux.HomeSystem.Web.Pages.CmsKit;
using Volo.CmsKit.Reactions;
using Ediux.HomeSystem.Web.Pages.CmsKit.Icons;
using Volo.CmsKit.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.CmsKit.Permissions;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using Volo.CmsKit.Public;
using Volo.CmsKit;
using Markdig;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Pages;
using Ediux.HomeSystem.Options;
using Ediux.HomeSystem.Web.Extensions;
using Ediux.HomeSystem.Web.Pages.Components.ABPHelpWidget;
using Ediux.HomeSystem.Web.Pages.Components.WelcomeWidget;
using Microsoft.Extensions.Localization;
using Ediux.HomeSystem.Extensions;
using Ediux.HomeSystem.Settings;
using Ediux.HomeSystem.Web.Pages.Components.TabViewerWidget;
using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.Web.Models.JSONData;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Emailing;
using Volo.Abp.AspNetCore.Mvc.UI.Components.LayoutHook;
using Ediux.HomeSystem.Web.Pages.Components.Firebase;
using Ediux.HomeSystem.Web.Pages.Components.WebManifest;
using Volo.Abp.BackgroundWorkers;
using Ediux.HomeSystem.Web.Jobs;

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
    [DependsOn(typeof(AbpBlobStoringFileSystemModule))]
    public class HomeSystemWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                   typeof(CmsKitResource),
                   typeof(CmsKitAdminApplicationContractsModule).Assembly,
                   typeof(CmsKitPublicApplicationContractsModule).Assembly,
                   typeof(CmsKitCommonApplicationContractsModule).Assembly
               );

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
            var configuration = context.Services.GetConfiguration();

            ConfigureUrls(configuration);
            ConfigureBundles();
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();

            ConfigureLocalizationServices();
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
            ConfigureCMSKit(context);
            ConfigureBlob(context);
            ConfigureSettingModule(context);
            ConfigureErrorHandle();
            ConfigureWidgets(context);
#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#else
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, Volo.Abp.MailKit.MailKitSmtpEmailSender>());
#endif
            Configure<AbpLayoutHookOptions>(options =>
            {
                options.Add(
                    LayoutHooks.Head.Last, //The hook name
                    typeof(WebManifestViewComponent) //The component to add
                );

                options.Add(
                    LayoutHooks.Body.Last, //The hook name
                    typeof(FirebaseViewComponent) //The component to add
                );

                options.Add(
                     LayoutHooks.Head.Last,
                     typeof(FirebaseHeaderViewComponent));
            });
        }

        private void ConfigureWidgets(ServiceConfigurationContext context)
        {
            Configure<DashboardWidgetOptions>(option =>
            {
                var localizerProvider = context.Services.GetRequiredService<IStringLocalizerFactory>();
                option.LocalizerProvider = localizerProvider;

                option.Add<ABPHelpWidgetViewComponent>("ABP Framework ReadMe Widget");
                string defaultSloganHtml = @"<div class=""p-5 text-center"">
    <div class=""d-inline-block bg-success text-white p-1 h5 rounded mb-4 "" role=""alert"">
		<h5 class=""m-1""> <i class=""fas fa-rocket""></i> Congratulations, <strong id=""websitename"">HomeSystem</strong> is successfully running!</h5>
	</div>
	<h1>Welcome to the Application</h1>
	<p class=""lead px-lg-5 mx-lg-5"" id=""welcome_area"">
	</p>
</div>";
                option.Add<WelcomeWidgetViewComponent>("Welcome Slogan Widget", DefaultEnabled: true)
                    .SetGlobalSettingName(HomeSystemSettings.WelcomeSlogan, defaultSloganHtml)
                    .UseSettingManagementUI();

                option.Add<TabViewerWidgetViewComponent>("Tab Viewer Widget", DefaultEnabled: true)
                    .SetPermissionName(HomeSystemPermissions.TabViewerWidget.Prefix)
                    .SetGlobalSettingName(HomeSystemSettings.TabViewGlobalSetting, System.Text.Json.JsonSerializer.Serialize(new TabViewPageSetting[] { }));
            });
        }

        private void ConfigureCMSKit(ServiceConfigurationContext context)
        {
            Configure<CmsKitUiOptions>(options =>
            {
                options.ReactionIcons[StandardReactions.Smile] = new LocalizableIconDictionary("fas fa-smile text-warning");
                options.ReactionIcons[StandardReactions.ThumbsUp] = new LocalizableIconDictionary("fa fa-thumbs-up text-primary");
                options.ReactionIcons[StandardReactions.Confused] = new LocalizableIconDictionary("fas fa-surprise text-warning");
                options.ReactionIcons[StandardReactions.Eyes] = new LocalizableIconDictionary("fas fa-meh-rolling-eyes text-warning");
                options.ReactionIcons[StandardReactions.Heart] = new LocalizableIconDictionary("fa fa-heart text-danger");
                options.ReactionIcons[StandardReactions.HeartBroken] = new LocalizableIconDictionary("fas fa-heart-broken text-danger");
                options.ReactionIcons[StandardReactions.Wink] = new LocalizableIconDictionary("fas fa-grin-wink text-warning");
                options.ReactionIcons[StandardReactions.Pray] = new LocalizableIconDictionary("fas fa-praying-hands text-info");
                options.ReactionIcons[StandardReactions.Rocket] = new LocalizableIconDictionary("fa fa-rocket text-success");
                options.ReactionIcons[StandardReactions.ThumbsDown] = new LocalizableIconDictionary("fa fa-thumbs-down text-secondary");
                options.ReactionIcons[StandardReactions.Victory] = new LocalizableIconDictionary("fas fa-hand-peace text-warning");
                options.ReactionIcons[StandardReactions.Rock] = new LocalizableIconDictionary("fas fa-hand-rock text-warning");
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Tags/", CmsKitAdminPermissions.Tags.Default);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Tags/CreateModal", CmsKitAdminPermissions.Tags.Create);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Tags/UpdateModal", CmsKitAdminPermissions.Tags.Update);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Pages", CmsKitAdminPermissions.Pages.Default);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Pages/Create", CmsKitAdminPermissions.Pages.Create);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Pages/Update", CmsKitAdminPermissions.Pages.Update);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Blogs", CmsKitAdminPermissions.Blogs.Default);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Blogs/Create", CmsKitAdminPermissions.Blogs.Create);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Blogs/Update", CmsKitAdminPermissions.Blogs.Update);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/BlogPosts", CmsKitAdminPermissions.BlogPosts.Default);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/BlogPosts/Create", CmsKitAdminPermissions.BlogPosts.Create);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/BlogPosts/Update", CmsKitAdminPermissions.BlogPosts.Update);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Comments/", CmsKitAdminPermissions.Comments.Default);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Comments/Details", CmsKitAdminPermissions.Comments.Default);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Menus", CmsKitAdminPermissions.Menus.Default);
                options.Conventions.AuthorizePage("/CmsKit/Admins/Menus/MenuItems/CreateModal", CmsKitAdminPermissions.Menus.Create);
                options.Conventions.AuthorizePage("/CmsKit/Admins/Menus/MenuItems/UpdateModal", CmsKitAdminPermissions.Menus.Update);
                options.Conventions.AuthorizeFolder("/CmsKit/Admins/Menus/MenuItems", CmsKitAdminPermissions.Menus.Update);
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AddPageRoute("/CmsKit/Admins/Tags/Index", "/Cms/Tags");
                options.Conventions.AddPageRoute("/CmsKit/Admins/Pages/Index", "/Cms/Pages");
                options.Conventions.AddPageRoute("/CmsKit/Admins/Pages/Create", "/Cms/Pages/Create");
                options.Conventions.AddPageRoute("/CmsKit/Admins/Pages/Update", "/Cms/Pages/Update/{Id}");
                options.Conventions.AddPageRoute("/CmsKit/Admins/Blogs/Index", "/Cms/Blogs");
                options.Conventions.AddPageRoute("/CmsKit/Admins/BlogPosts/Index", "/Cms/BlogPosts");
                options.Conventions.AddPageRoute("/CmsKit/Admins/BlogPosts/Create", "/Cms/BlogPosts/Create");
                options.Conventions.AddPageRoute("/CmsKit/Admins/BlogPosts/Update", "/Cms/BlogPosts/Update/{Id}");
                options.Conventions.AddPageRoute("/CmsKit/Admins/Comments/Index", "/Cms/Comments");
                options.Conventions.AddPageRoute("/CmsKit/Admins/Comments/Details", "/Cms/Comments/{Id}");
                options.Conventions.AddPageRoute("/CmsKit/Admins/Menus/MenuItems/Index", "/Cms/Menus/Items");
            });

            Configure<AbpPageToolbarOptions>(options =>
            {
                options.Configure<Pages.CmsKit.Admins.Tags.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<CmsKitResource>("NewTag"),
                            icon: "plus",
                            name: "NewButton",
                            requiredPolicyName: CmsKitAdminPermissions.Tags.Create
                        );
                    }
                );

                options.Configure<Pages.CmsKit.Admins.Pages.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<CmsKitResource>("NewPage"),
                            icon: "plus",
                            name: "CreatePage",
                            requiredPolicyName: CmsKitAdminPermissions.Pages.Create
                        );
                    });

                options.Configure<Pages.CmsKit.Admins.Blogs.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<CmsKitResource>("NewBlog"),
                            icon: "plus",
                            name: "CreateBlog",
                            id: "CreateBlog",
                            requiredPolicyName: CmsKitAdminPermissions.Blogs.Create
                            );
                    });

                options.Configure<Pages.CmsKit.Admins.BlogPosts.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<CmsKitResource>("NewBlogPost"),
                            icon: "plus",
                            name: "CreateBlogPost",
                            id: "CreateBlogPost",
                            requiredPolicyName: CmsKitAdminPermissions.BlogPosts.Create
                            );
                    });

                options.Configure<Pages.CmsKit.Admins.Menus.MenuItems.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<CmsKitResource>("NewMenuItem"),
                            icon: "plus",
                            name: "CreateMenuItem",
                            id: "CreateMenuItem",
                            requiredPolicyName: CmsKitAdminPermissions.Menus.Update
                            );
                    });
            });

            context.Services
              .AddSingleton(_ => new MarkdownPipelineBuilder()
                  .UseAutoLinks()
                  .UseBootstrap()
                  .UseGridTables()
                  .UsePipeTables()
                  .Build());
        }

        private void ConfigureBlob(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureVirtualFileSystem(hostingEnvironment);

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
        }

        private void ConfigureErrorHandle()
        {
            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("HomeSystem", typeof(HomeSystemResource));
            });
        }

        private void ConfigureSettingModule(ServiceConfigurationContext context)
        {
            Configure<SettingManagementPageOptions>(options =>
            {
                options.Contributors.Add(new WebSiteSettingPageContributor(context.Services.GetRequiredService<IAuthorizationService>()));
                options.Contributors.Add(new DashboardWidgetSettingPageContributor(context.Services.GetRequiredService<IAuthorizationService>()));
                options.Contributors.Add(new FCMSettingPageContributor(context.Services.GetRequiredService<IStringLocalizer<HomeSystemResource>>(), context.Services.GetRequiredService<IAuthorizationService>()));
                options.Contributors.Add(new BatchSettingsPageContributor(context.Services.GetRequiredService<IAuthorizationService>()));
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
                            configuration.AddFiles("/Components/DashboardWidgetSettingsGroup/Default.js");
                            configuration.AddFiles("/Components/WebSettingsGroup/Default.js");
                            configuration.AddFiles("/Components/FCMSettingGroup/Default.js");
                            configuration.AddFiles("/Components/BatchSettingGroup/Default.js");
                            configuration.AddFiles(
                               "/custlibs/ckeditor/ckeditor.js",
                               "/custlibs/ckeditor/easyLoadCKEditor.js",
                               "/custlibs/ckeditor/translations/ja.js");
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
                           configuration.AddFiles("/custlibs/fullcalendar/main.js",
                               "/custlibs/fullcalendar/locales-all.min.js");
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

                           configuration.AddFiles(
                               "/custlibs/ckeditor/ckeditor.js",
                               "/custlibs/ckeditor/easyLoadCKEditor.js",
                               "/custlibs/ckeditor/translations/ja.js");

                       });
                options.ScriptBundles
                    .Configure(typeof(Pages.IndexModel).FullName,
                        configuration =>
                        {
                            configuration.AddFiles("/Pages/Components/WelcomeWidget/Default.js");
                        });

                options.MinificationIgnoredFiles.Add("/custlibs/ckeditor/ckeditor.js");
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
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HomeSystemDomainSharedModule>();
                options.FileSets.AddEmbedded<HomeSystemDomainModule>();
                options.FileSets.AddEmbedded<HomeSystemApplicationContractsModule>();
                options.FileSets.AddEmbedded<HomeSystemApplicationModule>();
                options.FileSets.AddEmbedded<HomeSystemWebModule>();

                if (hostingEnvironment.IsDevelopment())
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Ediux.HomeSystem.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Ediux.HomeSystem.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Ediux.HomeSystem.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Ediux.HomeSystem.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HomeSystemWebModule>(hostingEnvironment.ContentRootPath);
                }
            });
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
                options.MenuContributors.Add(new CmsKitAdminMenuContributor());
                options.MenuContributors.Add(new CmsKitPublicMenuContributor());
                //options.MainMenuNames.Add(CmsKitMenus.Public);
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

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            if (GlobalFeatureManager.Instance.IsEnabled<PagesFeature>())
            {
                Configure<RazorPagesOptions>(options =>
                {
                    options.Conventions.AddPageRoute("/CmsKit/Public/Pages/Index", PageConsts.UrlPrefix + "{slug:minlength(1)}");
                    options.Conventions.AddPageRoute("/CmsKit/Public/Blogs/Index", @"/blogs/{blogSlug:minlength(1)}");
                    options.Conventions.AddPageRoute("/CmsKit/Public/Blogs/BlogPost", @"/blogs/{blogSlug}/{blogPostSlug:minlength(1)}");
                });
            }
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
            app.UseHttpsRedirection();
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

            context.AddBackgroundWorker<BackgroupJobSchedulerWorker>();
        }
    }
}

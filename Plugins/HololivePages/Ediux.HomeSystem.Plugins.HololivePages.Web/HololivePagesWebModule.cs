using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Ediux.HomeSystem.Plugins.HololivePages.Localization;
using Ediux.HomeSystem.Plugins.HololivePages.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Ediux.HomeSystem.Plugins.HololivePages.Permissions;

namespace Ediux.HomeSystem.Plugins.HololivePages.Web
{
    [DependsOn(
        typeof(HololivePagesHttpApiModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule)
        )]
    public class HololivePagesWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(HololivePagesResource), typeof(HololivePagesWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(HololivePagesWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new HololivePagesMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HololivePagesWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<HololivePagesWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HololivePagesWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
        }
    }
}

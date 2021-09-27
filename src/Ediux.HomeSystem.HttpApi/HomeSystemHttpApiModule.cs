using Localization.Resources.AbpUi;
using Ediux.HomeSystem.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Microsoft.AspNetCore.Hosting;
using Volo.Docs;
using Volo.Docs.Admin;
using Volo.CmsKit;

namespace Ediux.HomeSystem
{
    [DependsOn(
        typeof(HomeSystemApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule),
        typeof(AbpAutoMapperModule)
        )]
    [DependsOn(typeof(DocsHttpApiModule), typeof(DocsAdminHttpApiModule))]
    [DependsOn(typeof(CmsKitHttpApiModule))]
    public class HomeSystemHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(s => new PluginModuleCreateOrUpdateDTO() { HostEnvironment = s.GetService<IWebHostEnvironment>() });

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HomeSystemHttpApiModule>();
            });

            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<HomeSystemResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}

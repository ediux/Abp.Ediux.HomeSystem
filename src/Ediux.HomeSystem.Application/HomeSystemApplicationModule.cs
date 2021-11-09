using Ediux.HomeSystem.ApplicationPluginsManager;

using Microsoft.Extensions.DependencyInjection;

using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Docs;
using Volo.Docs.Admin;
using Volo.CmsKit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ediux.HomeSystem.SettingManagement;

namespace Ediux.HomeSystem
{
    [DependsOn(        
        typeof(HomeSystemDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(HomeSystemApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule)
        )]
    [DependsOn(typeof(DocsApplicationModule),typeof(DocsAdminApplicationModule))]
    [DependsOn(typeof(CmsKitApplicationModule))]
    public class HomeSystemApplicationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.AddTransient<IApplicationPluginsManager, ApplicationPluginsManager.ApplicationPluginsManager>();
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.Replace(ServiceDescriptor.Singleton<ISettingManager, SettingManagementAppService>());

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HomeSystemApplicationModule>();
            });
        }
    }
}

using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Docs;
using Volo.Docs.Admin;
using Volo.CmsKit;

namespace Ediux.HomeSystem
{
    [DependsOn(
        typeof(HomeSystemDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule)
    )]
    [DependsOn(typeof(DocsApplicationContractsModule))]
    [DependsOn(typeof(DocsAdminApplicationContractsModule))]
    [DependsOn(typeof(CmsKitApplicationContractsModule))]
    public class HomeSystemApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            HomeSystemDtoExtensions.Configure();
        }
    }
}

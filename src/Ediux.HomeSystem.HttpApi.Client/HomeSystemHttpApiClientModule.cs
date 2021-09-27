using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.SettingManagement;
using Volo.Docs;
using Volo.Docs.Admin;
using Volo.CmsKit;

namespace Ediux.HomeSystem
{
    [DependsOn(
        typeof(HomeSystemApplicationContractsModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule),
        typeof(AbpSettingManagementHttpApiClientModule)
    )]
    [DependsOn(typeof(DocsHttpApiClientModule),typeof(DocsAdminHttpApiClientModule))]
    [DependsOn(typeof(CmsKitHttpApiClientModule))]
    public class HomeSystemHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(HomeSystemApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}

using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Ediux.HomeSystem.Plugins.HololivePages
{
    [DependsOn(
        typeof(HololivePagesDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class HololivePagesApplicationContractsModule : AbpModule
    {

    }
}

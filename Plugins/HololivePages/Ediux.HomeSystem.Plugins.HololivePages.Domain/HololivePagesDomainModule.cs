using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Ediux.HomeSystem.Plugins.HololivePages
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(HololivePagesDomainSharedModule)
    )]
    public class HololivePagesDomainModule : AbpModule
    {

    }
}

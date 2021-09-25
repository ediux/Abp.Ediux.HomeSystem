using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Ediux.ABP.Features.SmartAdminUI
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(SmartAdminUIDomainSharedModule)
    )]
    public class SmartAdminUIDomainModule : AbpModule
    {

    }
}

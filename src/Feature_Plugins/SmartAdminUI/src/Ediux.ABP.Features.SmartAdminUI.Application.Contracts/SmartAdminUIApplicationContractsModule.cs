using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Ediux.ABP.Features.SmartAdminUI
{
    [DependsOn(
        typeof(SmartAdminUIDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class SmartAdminUIApplicationContractsModule : AbpModule
    {

    }
}

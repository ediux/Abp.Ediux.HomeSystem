using Volo.Abp.Modularity;

namespace Ediux.ABP.Features.SmartAdminUI
{
    [DependsOn(
        typeof(SmartAdminUIApplicationModule),
        typeof(SmartAdminUIDomainTestModule)
        )]
    public class SmartAdminUIApplicationTestModule : AbpModule
    {

    }
}

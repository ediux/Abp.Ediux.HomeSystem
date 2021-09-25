using Volo.Abp.Modularity;

namespace Ediux.HomeSystem
{
    [DependsOn(
        typeof(HomeSystemApplicationModule),
        typeof(HomeSystemDomainTestModule)
        )]
    public class HomeSystemApplicationTestModule : AbpModule
    {

    }
}
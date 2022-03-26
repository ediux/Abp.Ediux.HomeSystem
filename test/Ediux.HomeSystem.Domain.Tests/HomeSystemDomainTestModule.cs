using Ediux.HomeSystem.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ediux.HomeSystem;

[DependsOn(
    typeof(HomeSystemEntityFrameworkCoreTestModule)
    )]
public class HomeSystemDomainTestModule : AbpModule
{

}

using Ediux.HomeSystem.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Ediux.HomeSystem.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(HomeSystemEntityFrameworkCoreModule),
        typeof(HomeSystemApplicationContractsModule)
        )]
    public class HomeSystemDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}

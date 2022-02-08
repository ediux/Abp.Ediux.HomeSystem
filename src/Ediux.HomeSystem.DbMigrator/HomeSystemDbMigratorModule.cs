using Ediux.HomeSystem.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
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
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.Replace(ServiceDescriptor.Transient<IConnectionStringResolver, AddInsDbContextConnectionStringResolver>());
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);

            context.Services.AddHostedService<DbMigratorHostedService>();
        }
    }
}

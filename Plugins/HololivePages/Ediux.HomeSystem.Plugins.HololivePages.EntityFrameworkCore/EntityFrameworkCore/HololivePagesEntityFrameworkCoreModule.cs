using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ediux.HomeSystem.Plugins.HololivePages.EntityFrameworkCore
{
    [DependsOn(
        typeof(HololivePagesDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class HololivePagesEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<HololivePagesDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ediux.ABP.Features.SmartAdminUI.EntityFrameworkCore
{
    [DependsOn(
        typeof(SmartAdminUIDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class SmartAdminUIEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SmartAdminUIDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}
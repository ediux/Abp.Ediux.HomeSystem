using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Ediux.ABP.Features.SmartAdminUI.MongoDB
{
    [DependsOn(
        typeof(SmartAdminUIDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class SmartAdminUIMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<SmartAdminUIMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}

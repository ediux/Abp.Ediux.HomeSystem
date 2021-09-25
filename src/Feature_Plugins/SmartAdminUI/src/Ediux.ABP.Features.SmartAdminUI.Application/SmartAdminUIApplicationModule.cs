using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Ediux.ABP.Features.SmartAdminUI
{
    [DependsOn(
        typeof(SmartAdminUIDomainModule),
        typeof(SmartAdminUIApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class SmartAdminUIApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<SmartAdminUIApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SmartAdminUIApplicationModule>(validate: true);
            });
        }
    }
}

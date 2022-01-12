using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Ediux.HomeSystem.Plugins.HololivePages
{
    [DependsOn(
        typeof(HololivePagesDomainModule),
        typeof(HololivePagesApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class HololivePagesApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<HololivePagesApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HololivePagesApplicationModule>(validate: true);
            });
        }
    }
}

using Localization.Resources.AbpUi;
using Ediux.HomeSystem.Plugins.HololivePages.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages
{
    [DependsOn(
        typeof(HololivePagesApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class HololivePagesHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(HololivePagesHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<HololivePagesResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}

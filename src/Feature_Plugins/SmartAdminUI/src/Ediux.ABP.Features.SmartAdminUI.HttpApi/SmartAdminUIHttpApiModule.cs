using Localization.Resources.AbpUi;
using Ediux.ABP.Features.SmartAdminUI.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Ediux.ABP.Features.SmartAdminUI
{
    [DependsOn(
        typeof(SmartAdminUIApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class SmartAdminUIHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartAdminUIHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<SmartAdminUIResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}

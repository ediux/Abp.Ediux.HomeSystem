using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Ediux.ABP.Features.SmartAdminUI.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Ediux.ABP.Features.SmartAdminUI
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class SmartAdminUIDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SmartAdminUIDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<SmartAdminUIResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/SmartAdminUI");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("SmartAdminUI", typeof(SmartAdminUIResource));
            });
        }
    }
}

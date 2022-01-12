using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Ediux.HomeSystem.Plugins.HololivePages.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Ediux.HomeSystem.Plugins.HololivePages
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class HololivePagesDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HololivePagesDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<HololivePagesResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/HololivePages");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("HololivePages", typeof(HololivePagesResource));
            });
        }
    }
}

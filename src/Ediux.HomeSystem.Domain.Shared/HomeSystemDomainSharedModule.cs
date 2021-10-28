
using Ediux.HomeSystem.Localization;

using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Docs;
using Volo.CmsKit;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Ediux.HomeSystem
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpTenantManagementDomainSharedModule),
        typeof(AbpAutoMapperModule)
        )]
    [DependsOn(typeof(DocsDomainSharedModule))]
    [DependsOn(typeof(CmsKitDomainSharedModule))]
    [DependsOn(typeof(AbpBlobStoringModule))]
    public class HomeSystemDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            HomeSystemGlobalFeatureConfigurator.Configure();
            HomeSystemModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HomeSystemDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<HomeSystemResource>("zh-Hant")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/HomeSystem");

                options.DefaultResourceType = typeof(HomeSystemResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("HomeSystem", typeof(HomeSystemResource));
            });
          
        
        }
    }
}

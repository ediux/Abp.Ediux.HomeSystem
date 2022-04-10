using Ediux.HomeSystem.BlobContainers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.IO;

using Volo.Abp.Account;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
namespace Ediux.HomeSystem
{
    [DependsOn(
        typeof(HomeSystemDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule)
    )]
    [DependsOn(typeof(AbpBlobStoringModule))]
    [DependsOn(typeof(AbpBlobStoringFileSystemModule))]
    public class HomeSystemApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            HomeSystemDtoExtensions.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.Configure<MediaContainer>(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = path;
                    });
                });

                options.Containers.Configure<PluginsContainer>(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = path;
                    });
                });
            });
        }
    }
}

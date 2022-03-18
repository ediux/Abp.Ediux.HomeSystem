
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Volo.Abp.Threading;
using Volo.Abp.ObjectExtending;
using Ediux.HomeSystem.SystemManagement;

namespace Ediux.HomeSystem
{
    public static class ConfigurationExtendsion
    {
        private static ILogger logger = logger ?? (new LoggerConfiguration()).CreateLogger();

        public static void ConfigureABPPlugins(this AbpApplicationCreationOptions options)
        {
            try
            {
                CollectiblePluginInSource collectiblePluginInSource =
                   options.Services.GetRequiredService<CollectiblePluginInSource>();

                options.PlugInSources.Add(collectiblePluginInSource);
            }
            catch (Exception ex)
            {
                logger?.Error(ex, "Init Plugins Failed!");
            }

        }

        public static void ConfigureABPPlugins(this AbpApplicationCreationOptions options, IWebHostEnvironment env)
        {
            try
            {
                CollectiblePluginInSource collectiblePluginInSource =
                    options.Services.GetRequiredService<CollectiblePluginInSource>();

                options.PlugInSources.Add(collectiblePluginInSource);
            }
            catch (Exception ex)
            {
                logger?.Error(ex, "Init Plugins Failed!");
            }
        }

        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /* You can add extension properties to DTOs
                 * defined in the depended modules.
                 *
                 * Example:
                 *
                 * ObjectExtensionManager.Instance
                 *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
                 *
                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Object-Extensions
                 */

                ObjectExtensionManager.Instance
                    .AddOrUpdateProperty<File_Store, string>("Description");
              
                ObjectExtensionManager.Instance
                    .AddOrUpdateProperty<File_Store, SMBStoreInformation>("ShareInformation", option => { option.DefaultValue = new SMBStoreInformation(); });

            });
        }
    }
}

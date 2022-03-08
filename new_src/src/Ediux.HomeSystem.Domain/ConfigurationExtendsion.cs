
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp;
using Serilog;
using Microsoft.AspNetCore.Hosting;

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
    }
}

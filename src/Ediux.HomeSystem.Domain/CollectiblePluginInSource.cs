using Ediux.HomeSystem.Services;
using Ediux.HomeSystem.SystemManagement;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Modularity.PlugIns;

namespace Ediux.HomeSystem
{
    public class CollectiblePluginInSource : IPlugInSource, ITransientDependency
    {
        [Inject] public PluginsManager PluginsManager { get; set; }

        [Inject] public ILogger<CollectiblePluginInSource> Logger { get; set; }

        public CollectiblePluginInSource()
        {
            Logger = Logger ?? NullLogger<CollectiblePluginInSource>.Instance;
        }

      

        public Type[] GetModules()
        {
            var modules = PluginsManager.GetModules();

            if (modules.Any() == false)
            {
                var waitTask = PluginsManager.ReadConfigurationAsync();
                waitTask.Wait();
                modules = PluginsManager.GetModules();
                PluginsManager.LoadPluginsAsync().Wait();
            }

            
            //var jsonText = File.ReadAllText(Path.Combine(RootPath, "plugins.json"));
            //var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptionsDTOs>(jsonText);

            //string pluginFolderPath = Path.Combine(RootPath, "Plugins");

           

            //if (positionOptions != null)
            //{
            //    if (positionOptions != null && positionOptions.plugins.Count() > 0)
            //    {
            //       

            //        bool autoFix = false;

           

            //        if (autoFix)
            //        {
            //            string strJson = System.Text.Json.JsonSerializer.Serialize(positionOptions, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
            //            File.WriteAllText(Path.Combine(RootPath, "plugins.json"), strJson, System.Text.Encoding.UTF8);
            //        }
            //    }
            //}

            return modules.ToArray();
        }
    }
}

using Ediux.HomeSystem.Settings;

using JetBrains.Annotations;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Modularity.PlugIns;

namespace Ediux.HomeSystem
{
    public class CollectiblePluginInSource : IPlugInSource, ITransientDependency
    {
        public string RootPath { get; private set; }
        public ILogger<CollectiblePluginInSource> Logger { get; set; }

        public CollectiblePluginInSource([NotNull] string rootPath)
        {
            RootPath = rootPath;
            Logger = NullLogger<CollectiblePluginInSource>.Instance;
        }

        private void LoadFile(CollectibleAssemblyLoadContext collectibleAssemblyLoadContext, string filepath, IList<Type> pluginTypes)
        {
            try
            {
                Assembly loadedAssembly = null;

                using (FileStream ms = File.OpenRead(filepath))
                {
                    loadedAssembly = collectibleAssemblyLoadContext.LoadFromStream(ms);
                    ms.Close();
                }

                try
                {
                    foreach (var type in loadedAssembly.GetTypes())
                    {
                        if (AbpModule.IsAbpModule(type))
                        {
                            pluginTypes.AddIfNotContains(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Could not get module types from assembly: " + loadedAssembly.FullName);
                    throw new AbpException("Could not get module types from assembly: " + loadedAssembly.FullName, ex);
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new AbpException($"[{ex.TargetSite.Name}] Has occur error.", ex);
            }
        }

        public Type[] GetModules()
        {
            var modules = new List<Type>();
            var jsonText = File.ReadAllText(Path.Combine(RootPath, "plugins.json"));
            var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptionsDTOs>(jsonText);

            string pluginFolderPath = Path.Combine(RootPath, "Plugins");

            if (Directory.Exists(pluginFolderPath) == false)
            {
                Directory.CreateDirectory(pluginFolderPath);
            }

            if (positionOptions != null)
            {
                if (positionOptions != null && positionOptions.plugins.Count() > 0)
                {
                    PluginsDataDTO[] loadPath = positionOptions.plugins.Where(a => a.Disabled == false).ToArray();

                    bool autoFix = false;

                    foreach (PluginsDataDTO path in loadPath)
                    {
                        string loadFn = path.PluginPath;

                        try
                        {

                            if (Path.IsPathRooted(loadFn) == false)
                            {
                                loadFn = RootPath.urlConvertorLocal(path.PluginPath);
                            }

                            if (File.Exists(loadFn) == false)
                                continue;

                            string pluginsFolderPath = Path.Combine(Path.GetDirectoryName(loadFn), Path.GetFileNameWithoutExtension(loadFn));
                            CollectibleAssemblyLoadContext collectibleAssemblyLoadContext = new CollectibleAssemblyLoadContext(pluginsFolderPath);

                            if (Path.GetExtension(loadFn).ToUpperInvariant() == ".ZIP")
                            {

                                if (Directory.Exists(pluginsFolderPath) == false)
                                {
                                    Directory.CreateDirectory(pluginsFolderPath);
                                }

                                string[] extraTargets = Directory.GetFiles(pluginsFolderPath)
                                    .WhereIf(true, p => Path.GetExtension(p).ToUpperInvariant() == ".DLL").ToArray();

                                if (extraTargets.Count() == 0)
                                {
                                    ZipFile.ExtractToDirectory(path.PluginPath, pluginsFolderPath, true);  //解開ZIP檔案到這個資料夾
                                    extraTargets = Directory.GetFiles(pluginsFolderPath)
                                        .WhereIf(true, fn => Path.GetExtension(fn).ToUpperInvariant() == ".DLL")
                                        .ToArray();
                                }

                                foreach (var loadfn in extraTargets)
                                {
                                    LoadFile(collectibleAssemblyLoadContext, loadfn, modules);
                                }

                            }
                            else
                            {
                                pluginsFolderPath = Path.GetDirectoryName(loadFn);
                                string[] loadfiles = Directory.GetFiles(pluginsFolderPath, "*.dll", SearchOption.AllDirectories);
                               
                                if (loadfiles.Any())
                                {
                                    foreach (var loadfile in loadfiles)
                                    {
                                        if (File.Exists(loadfile) == false)
                                            continue;

                                        LoadFile(collectibleAssemblyLoadContext, loadfile, modules);
                                    }
                                }
                               
                            }

                            if (Path.IsPathRooted(path.PluginPath))
                            {
                                path.PluginPath = RootPath.urlConvertor(path.PluginPath);
                                autoFix = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger?.LogError(ex, "Loading assembly failed.");
                            continue;
                        }
                    }

                    if (autoFix)
                    {
                        string strJson = System.Text.Json.JsonSerializer.Serialize(positionOptions, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
                        File.WriteAllText(Path.Combine(RootPath, "plugins.json"), strJson, System.Text.Encoding.UTF8);
                    }
                }
            }

            return modules.ToArray();
        }
    }
}

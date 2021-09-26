using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Hosting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Modularity.PlugIns;

namespace Ediux.HomeSystem
{
    public static class AssemblyLoaderManager
    {
        private static readonly Dictionary<string, CollectibleAssemblyLoadContext> _pluginAssemblies = _pluginAssemblies ?? new Dictionary<string, CollectibleAssemblyLoadContext>();

        public static string FindLoadContext(string pathOrName)
        {
            return _pluginAssemblies.Keys.Where(a => Path.GetFileName(a) == Path.GetFileName(pathOrName)).SingleOrDefault();
        }

        public static Assembly LoadAssembly(in string assemblyName)
        {

            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName));

            if (!File.Exists(assemblyName))
                throw new FileNotFoundException($"Assembly file not found: {nameof(assemblyName)}");

            string assembly = assemblyName;

            if (!Path.IsPathRooted(assembly))
                assembly = Path.GetFullPath(assembly);

            var context = new CollectibleAssemblyLoadContext();
            Assembly loadedAssembly = null;

            if (_pluginAssemblies.ContainsKey(assembly) == false)
            {
                using (FileStream ms = File.OpenRead(assembly))
                {
                    loadedAssembly = context.LoadFromStream(ms);
                    ms.Close();
                }

                _pluginAssemblies.Add(loadedAssembly.FullName, context);
            }

            return loadedAssembly; // Assembly.LoadFrom(assembly);
        }

        public static bool PluginUnload(in string assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName));

            var fn = assemblyName;
            var module = _pluginAssemblies.Where(w => Path.GetFileName(w.Key) == Path.GetFileName(fn)).Select(s => new { key = s.Key, Module = s.Value }).SingleOrDefault();

            if (module != null)
            {
                if (module.Module != null)
                {
                    module.Module.Unload();
                    _pluginAssemblies.Remove(module.key);
                    return true;
                }
            }

            return false;
        }

        public static bool PluginUnload(in Assembly assembly)
        {
            string fullname = assembly.FullName;

            var module = _pluginAssemblies.Where(w => w.Key == fullname).Select(s => new { key = s.Key, Module = s.Value }).SingleOrDefault();

            if (module != null)
            {
                if (module.Module != null)
                {
                    module.Module.Unload();
                    _pluginAssemblies.Remove(module.key);
                    return true;
                }
            }

            return false;
        }
        public static void ConfigureABPPlugins(this AbpApplicationCreationOptions options, IWebHostEnvironment env)
        {
            var jsonText = File.ReadAllText(Path.Combine(env.ContentRootPath, "plugins.json"));
            var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptions>(jsonText);

            //var c = config.GetSection("plugins").GetChildren();

            //services.AddTransient<IApplicationPluginsManager, ApplicationPluginsManager.ApplicationPluginsManager>();
            string pluginFolderPath = Path.Combine(env.ContentRootPath, "Plugins");

            if (Directory.Exists(pluginFolderPath) == false)
            {
                Directory.CreateDirectory(pluginFolderPath);
            }

            if (positionOptions != null)
            {
                if (positionOptions != null && positionOptions.plugins.Count() > 0)
                {
                    string[] loadPath = positionOptions.plugins.Where(a => a.Disabled == false && File.Exists(a.PluginPath)).Select(s => s.PluginPath).ToArray();
                    IList<Type> pluginTypes = new List<Type>();

                    foreach (string path in loadPath)
                    {
                        try
                        {
                            string key = FindLoadContext(path);

                            if (string.IsNullOrWhiteSpace(key))
                            {
                                continue;
                            }

                            var findABPModules = _pluginAssemblies[key].Assemblies.SelectMany(s => s.GetTypes().Where(t => t.GetBaseClasses(typeof(AbpModule)) != null && t.IsAbstract == false).ToList());

                            if (findABPModules != null && findABPModules.Any())
                            {
                                foreach(Type t in findABPModules)
                                {
                                    pluginTypes.Add(t);
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    if (pluginTypes.Count() > 0)
                    {
                        TypePlugInSource typePlugInSource = new TypePlugInSource(pluginTypes.ToArray());
                        options.PlugInSources.Add(typePlugInSource);
                    }                    
                }
            }
        }
    }
}

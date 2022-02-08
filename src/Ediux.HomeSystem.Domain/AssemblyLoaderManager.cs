
using Microsoft.AspNetCore.Hosting;

using Serilog;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Volo.Abp;

namespace Ediux.HomeSystem
{
    public static class AssemblyLoaderManager
    {
        private static readonly Dictionary<string, CollectibleAssemblyLoadContext> _pluginAssemblies = _pluginAssemblies ?? new Dictionary<string, CollectibleAssemblyLoadContext>();
        private static ILogger logger = logger ?? (new LoggerConfiguration()).CreateLogger();


        public static string FindLoadContext(string pathOrName)
        {
            return _pluginAssemblies.Keys.Where(a => a == pathOrName).SingleOrDefault();
        }

        public static Assembly LoadAssembly(in string assemblyName, AbpApplicationCreationOptions options = null)
        {

            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName));

            if (!File.Exists(assemblyName))
                throw new FileNotFoundException($"Assembly file not found: {nameof(assemblyName)}");

            string assembly = assemblyName;

            if (!Path.IsPathRooted(assembly))
                assembly = Path.GetFullPath(assembly);

            var context = new CollectibleAssemblyLoadContext(assembly);

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

        public static void ConfigureABPPlugins(this AbpApplicationCreationOptions options,
           string rootPath)
        {
            try
            {
                CollectiblePluginInSource collectiblePluginInSource = 
                    new CollectiblePluginInSource(rootPath);

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
                    new CollectiblePluginInSource(env.ContentRootPath);

                options.PlugInSources.Add(collectiblePluginInSource);
            }
            catch (Exception ex)
            {
                logger?.Error(ex, "Init Plugins Failed!");
            }
        }
    }
}

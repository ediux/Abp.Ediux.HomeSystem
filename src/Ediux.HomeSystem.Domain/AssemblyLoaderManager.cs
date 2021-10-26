using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Modularity.PlugIns;

namespace Ediux.HomeSystem
{
    public static class AssemblyLoaderManager
    {
        private static readonly Dictionary<string, CollectibleAssemblyLoadContext> _pluginAssemblies = _pluginAssemblies ?? new Dictionary<string, CollectibleAssemblyLoadContext>();
        private static Serilog.ILogger logger = logger ?? (new LoggerConfiguration()).CreateLogger();


        public static string FindLoadContext(string pathOrName)
        {
            return _pluginAssemblies.Keys.Where(a => a == pathOrName).SingleOrDefault();
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
            var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptionsDTOs>(jsonText);

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
                    string[] loadPath = positionOptions.plugins.Where(a => a.Disabled == false).Select(s => s.PluginPath).ToArray();

                    IList<Type> pluginTypes = new List<Type>();

                    foreach (string path in loadPath)
                    {
                        try
                        {
                            if (File.Exists(path) == false)
                                continue;

                            if (Path.GetExtension(path).ToUpperInvariant() == ".ZIP")
                            {
                                string pluginsFolderPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));

                                if (Directory.Exists(pluginsFolderPath) == false)
                                {
                                    Directory.CreateDirectory(pluginsFolderPath);
                                }

                                string[] extraTargets = Directory.GetFiles(pluginsFolderPath)
                                    .WhereIf(true, p => Path.GetExtension(p).ToUpperInvariant() == ".DLL").ToArray();

                                if (extraTargets.Count() == 0)
                                {
                                    ZipFile.ExtractToDirectory(path, pluginsFolderPath, true);  //解開ZIP檔案到這個資料夾
                                    extraTargets = Directory.GetFiles(pluginsFolderPath)
                                        .WhereIf(true, fn => Path.GetExtension(fn).ToUpperInvariant() == ".DLL")
                                        .ToArray();
                                }

                                foreach (var loadfn in extraTargets)
                                {
                                    try
                                    {
                                        string key = FindLoadContext(loadfn);
                                        Assembly asm = null;
                                      
                                        if (string.IsNullOrWhiteSpace(key))
                                        {
                                            asm = LoadAssembly(loadfn);
                                            key = FindLoadContext(asm.FullName);
                                        }
                                      
                                        try
                                        {
                                            var findABPModules = _pluginAssemblies[key].Assemblies.SelectMany(s => s.GetTypes())
                                                .WhereIf(true,t => AbpModule.IsAbpModule(t))
                                                .ToList();

                                            if (findABPModules != null && findABPModules.Any())
                                            {
                                                foreach (Type t in findABPModules)
                                                {
                                                    pluginTypes.Add(t);
                                                }
                                            }
                                        }
                                        catch (Exception ex_i)
                                        {
                                            _pluginAssemblies[key].Unload();
                                            _pluginAssemblies.Remove(key);
                                            logger?.Error(ex_i, ex_i.Message);                                            
                                        }
                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        logger?.Error(ex, "Loading assembly failed.");
                                        continue;
                                    }
                                }

                            }
                            else
                            {
                                string key = FindLoadContext(path);

                                if (string.IsNullOrWhiteSpace(key))
                                {
                                    continue;
                                }

                                try
                                {
                                    var findABPModules = _pluginAssemblies[key].Assemblies.SelectMany(s => s.GetTypes())
                                                .WhereIf(true, t => AbpModule.IsAbpModule(t))
                                                .ToList();

                                    if (findABPModules != null && findABPModules.Any())
                                    {
                                        foreach (Type t in findABPModules)
                                        {
                                            try
                                            {
                                                pluginTypes.Add(t);
                                            }
                                            catch (Exception ex)
                                            {

                                                logger?.Error(ex, "Loading assembly failed.");
                                                continue;
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex_i)
                                {
                                    _pluginAssemblies[key].Unload();
                                    _pluginAssemblies.Remove(key);
                                    logger?.Error(ex_i, ex_i.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            logger?.Error(ex, "Loading assembly failed.");
                            continue;
                        }
                    }

                    if (pluginTypes.Count() > 0)
                    {
                        try
                        {
                            TypePlugInSource typePlugInSource = new TypePlugInSource(pluginTypes.ToArray());
                            options.PlugInSources.Add(typePlugInSource);
                        }
                        catch (Exception ex)
                        {
                            logger?.Error(ex, "Init Plugins Failed!");
                        }

                    }
                }
            }
        }
    }
}

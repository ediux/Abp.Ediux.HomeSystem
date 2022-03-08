using Ediux.HomeSystem.Options;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Modularity;

namespace Ediux.HomeSystem.Services
{
    public class PluginsManager : DomainService
    {
        private IOptions<PluginsOption> _options;
        private string _configFileName;

        private List<Type> _pluginTypes;
        private AssemblyLoadContext _loadContext;

        public PluginsManager(IOptions<PluginsOption> options)
        {
            _options = options;

            _configFileName = Path.Combine(_options.Value.RootPath, "plugins.json");
            _pluginTypes = new List<Type>();
            _loadContext = AssemblyLoadContext.Default;
            _loadContext.Resolving += _loadContext_Resolving;
        }

        private Assembly _loadContext_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            try
            {
                int index = _options.Value.Plugins.FindIndex(x => x.Name == arg2.Name);

                string loadLocation = string.Empty;

                if (index == -1)
                {
                    loadLocation = Path.Combine(_options.Value.RootPath, arg2.Name + ".dll");

                    if (File.Exists(loadLocation) == false)
                    {
                        loadLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Plugins", arg2.Name + ".dll");

                        if (File.Exists(loadLocation) == false)
                        {
                            throw new FileNotFoundException(arg2.Name + ".dll");
                        }
                    }

                }
                else
                {
                    loadLocation = _options.Value.Plugins[index].PluginPath;
                }

                Task<Assembly> loadTask = LoadAssemblyFileAsync(loadLocation);
                loadTask.Wait();

                return loadTask.Result;
            }
            catch (Exception ex)
            {
                Logger.LogError("CollectibleAssemblyLoadContext Error:" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 讀取擴充模組設定檔
        /// </summary>
        /// <returns></returns>
        public async Task<PluginsOptionsDTOs> ReadConfigurationAsync()
        {
            var jsonText = await File.ReadAllTextAsync(_configFileName);
            var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptionsDTOs>(jsonText);
            return positionOptions;
        }

        /// <summary>
        /// 寫入擴充模組設定檔
        /// </summary>
        /// <returns></returns>
        public async Task WriteConfigurationAsync()
        {
            if (Directory.Exists(_options.Value.RootPath) == false)
            {
                Directory.CreateDirectory(_options.Value.RootPath);
            }

            string config = System.Text.Json.JsonSerializer.Serialize(_options.Value.Plugins, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
            await File.WriteAllTextAsync(_configFileName, config, Encoding.UTF8);
        }

        public async Task LoadPluginsAsync()
        {
            PluginsDataDTO[] loadPath = _options.Value.Plugins.Where(a => a.Disabled == false).ToArray();

            foreach (PluginsDataDTO path in loadPath)
            {
                string loadFn = path.PluginPath;

                try
                {

                    if (Path.IsPathRooted(loadFn) == false)
                    {
                        loadFn = _options.Value.RootPath.urlConvertorLocal(path.PluginPath);
                    }

                    if (File.Exists(loadFn) == false)
                        continue;

                    await LoadAssemblyFileAsync(loadFn);
                    //string pluginsFolderPath = Path.Combine(Path.GetDirectoryName(loadFn), Path.GetFileNameWithoutExtension(loadFn));


                    //if (Path.GetExtension(loadFn).ToUpperInvariant() == ".ZIP")
                    //{

                    //    if (Directory.Exists(pluginsFolderPath) == false)
                    //    {
                    //        Directory.CreateDirectory(pluginsFolderPath);
                    //    }

                    //    string[] extraTargets = Directory.GetFiles(pluginsFolderPath)
                    //        .WhereIf(true, p => Path.GetExtension(p).ToUpperInvariant() == ".DLL").ToArray();

                    //    if (extraTargets.Count() == 0)
                    //    {
                    //        ZipFile.ExtractToDirectory(path.PluginPath, pluginsFolderPath, true);  //解開ZIP檔案到這個資料夾
                    //        extraTargets = Directory.GetFiles(pluginsFolderPath)
                    //            .WhereIf(true, fn => Path.GetExtension(fn).ToUpperInvariant() == ".DLL")
                    //            .ToArray();
                    //    }

                    //    foreach (var loadfn in extraTargets)
                    //    {
                    //        LoadFile(AssemblyLoadContext.Default, loadfn, modules);
                    //    }

                    //}
                    //else
                    //{
                    //pluginsFolderPath = Path.GetDirectoryName(loadFn);
                    //string[] loadfiles = Directory.GetFiles(pluginsFolderPath, "*.dll", SearchOption.AllDirectories);

                    //if (loadfiles.Any())
                    //{
                    //    foreach (var loadfile in loadfiles)
                    //    {
                    //        if (File.Exists(loadfile) == false)
                    //            continue;

                    //        LoadFile(AssemblyLoadContext.Default, loadfile, modules);
                    //    }
                    //}

                    //}

                    //if (Path.IsPathRooted(path.PluginPath))
                    //{
                    //    path.PluginPath = RootPath.urlConvertor(path.PluginPath);
                    //    autoFix = true;
                    //}
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "Loading assembly failed.");
                    continue;
                }
            }
        }
        public Task<Assembly> LoadAssemblyFileAsync(string filePath)
        {
            try
            {
                Assembly loadedAssembly = null;

                using (FileStream ms = File.OpenRead(filePath))
                {
                    loadedAssembly = _loadContext.LoadFromStream(ms);
                    ms.Close();
                }

                try
                {
                    foreach (var type in loadedAssembly.GetTypes())
                    {
                        if (AbpModule.IsAbpModule(type))
                        {
                            _pluginTypes.AddIfNotContains(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Could not get module types from assembly: " + loadedAssembly.FullName);
                    throw new AbpException("Could not get module types from assembly: " + loadedAssembly.FullName, ex);
                }

                return Task.FromResult(loadedAssembly);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new AbpException($"[{ex.TargetSite.Name}] Has occur error.", ex);
            }
        }

        public Type[] GetModules()
        {
            return _pluginTypes.ToArray();
        }
    }
}

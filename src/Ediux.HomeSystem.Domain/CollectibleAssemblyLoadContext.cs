using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Ediux.HomeSystem
{
    public class CollectibleAssemblyLoadContext : AssemblyLoadContext
    {
        private string loadAssemblyFolderLocation;
        private string currentLocation;

        public ILogger<CollectibleAssemblyLoadContext> Logger { get; set; }

        public CollectibleAssemblyLoadContext(string name, bool isCollectible = true) : base(name, isCollectible)
        {
            loadAssemblyFolderLocation = Path.GetDirectoryName(name);
            currentLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Init();
        }

        public CollectibleAssemblyLoadContext() : base(true)
        {
            currentLocation = loadAssemblyFolderLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Init();
        }

        private void Init()
        {
            Logger  = NullLogger<CollectibleAssemblyLoadContext>.Instance;
            this.Resolving += CollectibleAssemblyLoadContext_Resolving;
        }

        //protected override Assembly Load(AssemblyName assemblyName)
        //{
        //    Assembly assembly = base.Load(assemblyName);

        //    if (abpApplicationCreationOptions != null)
        //    {
        //        List<Type> types = new List<Type>();
        //        try
        //        {
        //            var findABPModules = Assemblies.SelectMany(s => s.GetTypes())
        //                            .Where(t => AbpModule.IsAbpModule(t))
        //                            .ToList();

        //            if (findABPModules != null && findABPModules.Any())
        //            {
        //                foreach (Type t in findABPModules)
        //                {
        //                    types.Add(t);
        //                }
        //            }

        //            TypePlugInSource typePlugInSource = new TypePlugInSource(types.ToArray());
        //            abpApplicationCreationOptions.PlugInSources.Add(typePlugInSource);
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }

        //    return assembly;
        //}

        private Assembly CollectibleAssemblyLoadContext_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        { 
            try
            {
                string loadLocation = Path.Combine(loadAssemblyFolderLocation, arg2.Name + ".dll");

                if (File.Exists(loadLocation) == false)
                {
                    loadLocation = Path.Combine(currentLocation, "Plugins", arg2.Name + ".dll");

                    if (File.Exists(loadLocation) == false)
                    {
                        loadLocation = Path.Combine(currentLocation, arg2.Name + ".dll");

                        if (File.Exists(loadLocation) == false)
                        {
                            throw new FileNotFoundException(arg2.Name + ".dll");
                        }
                    }
                }

                using (FileStream fs = File.OpenRead(loadLocation))
                {
                    Assembly loaded = arg1.LoadFromStream(fs);
                    fs.Close();
                    return loaded;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("CollectibleAssemblyLoadContext Error:" + ex.Message);
                return null;
            }
        }
    }
}

using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Ediux.HomeSystem.Models.jqDataTables;
using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Hosting;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.ApplicationPluginsManager
{
    public class ApplicationPluginsManager : CrudAppService<AbpPlugins, PluginModuleDTO, Guid>, IApplicationPluginsManager
    {

        private readonly ICurrentUser currentUser;
        private readonly IWebHostEnvironment env;

        public ApplicationPluginsManager(IRepository<AbpPlugins, Guid> repository, ICurrentUser currentUser, IWebHostEnvironment hostEnvironment) : base(repository)
        {
            this.currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            env = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
        }

        public async override Task<PluginModuleDTO> CreateAsync(PluginModuleDTO input)
        {
            string settingFilePath = Path.Combine(env.ContentRootPath, "plugins.json");
            var jsonText = File.ReadAllText(settingFilePath);
            var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptions>(jsonText);

            var plugins = positionOptions.plugins.ToList();

            var task = await base.CreateAsync(input);

            if (task.Disabled == false)
            {
                if (plugins.Any(w => w.Name == task.Name) == false)
                {
                    plugins.Add(new PluginsData() { Name = task.Name, Disabled = task.Disabled, PluginPath = task.PluginPath });
                }
                else
                {
                    int index = plugins.FindIndex(w => w.Name == task.Name);
                    plugins[index].Disabled = task.Disabled;
                    plugins[index].PluginPath = task.PluginPath;
                }

                positionOptions.plugins = plugins.ToArray();
                File.WriteAllText(settingFilePath, System.Text.Json.JsonSerializer.Serialize(positionOptions, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true }), System.Text.Encoding.UTF8);
            }

            return task;
        }

        public async override Task<PluginModuleDTO> UpdateAsync(Guid id, PluginModuleDTO input)
        {
            string settingFilePath = Path.Combine(env.ContentRootPath, "plugins.json");
            var jsonText = File.ReadAllText(settingFilePath);
            var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptions>(jsonText);

            var plugins = positionOptions.plugins.ToList();

            var task = await base.UpdateAsync(id, input);


            if (task.Disabled == false)
            {
                if (positionOptions.plugins.Any(a => a.Name == task.Name) == false)
                {
                    plugins.Add(new PluginsData() { Name = task.Name, Disabled = task.Disabled, PluginPath = task.PluginPath });
                    positionOptions.plugins = plugins.ToArray();
                }
                else
                {
                    var updateItem = positionOptions.plugins.SingleOrDefault(s => s.Name == task.Name);
                    plugins.Remove(updateItem);
                    updateItem.PluginPath = task.PluginPath;
                    plugins.Add(updateItem);
                    positionOptions.plugins = plugins.ToArray();
                }

                File.WriteAllText(settingFilePath, System.Text.Json.JsonSerializer.Serialize(positionOptions, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true }), System.Text.Encoding.UTF8);
            }
            else
            {
                var removeItem = positionOptions.plugins.SingleOrDefault(s => s.Name == task.Name);
                string unloadAssembly = AssemblyLoaderManager.FindLoadContext(removeItem.Name);

                if (!string.IsNullOrWhiteSpace(unloadAssembly))
                {
                    AssemblyLoaderManager.PluginUnload(AssemblyLoaderManager.FindLoadContext(removeItem.Name));
                }

                plugins.Remove(removeItem);
                File.WriteAllText(settingFilePath, System.Text.Json.JsonSerializer.Serialize(positionOptions, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true }), System.Text.Encoding.UTF8);
            }

            return task;
        }

        public async override Task DeleteAsync(Guid id)
        {
            var currentItem = await GetAsync(id);

            if (currentItem != null)
            {
                string settingFilePath = Path.Combine(env.ContentRootPath, "plugins.json");
                var jsonText = File.ReadAllText(settingFilePath);
                var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptions>(jsonText);

                var plugins = positionOptions.plugins.ToList();

                var removeItem = positionOptions.plugins.SingleOrDefault(s => s.Name == currentItem.Name);

                if (removeItem != null)
                {
                    string unloadAssembly = AssemblyLoaderManager.FindLoadContext(removeItem.Name);

                    if (!string.IsNullOrWhiteSpace(unloadAssembly))
                    {
                        AssemblyLoaderManager.PluginUnload(AssemblyLoaderManager.FindLoadContext(removeItem.Name));
                    }

                    plugins.Remove(removeItem);
                }

                File.WriteAllText(settingFilePath, System.Text.Json.JsonSerializer.Serialize(positionOptions, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true }), System.Text.Encoding.UTF8);
            }

            await base.DeleteAsync(id);
        }

    }
}

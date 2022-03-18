using Ediux.HomeSystem.Services;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Hosting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.ApplicationPluginsManager
{
    public class ApplicationPluginsManager : HomeSystemCrudAppService<AbpPlugins, PluginModuleDto, Guid, AbpSearchRequestDto>, IPluginsAppService
    {
        private readonly IWebHostEnvironment env;
        private readonly PluginsManager _pluginsManager;
        public ApplicationPluginsManager(IRepository<AbpPlugins, Guid> repository,
            PluginsManager pluginsManager,
            IWebHostEnvironment hostEnvironment) : base(repository)
        {
            env = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
            _pluginsManager = pluginsManager;
        }

        public async override Task<PagedResultDto<PluginModuleDto>> GetListAsync(AbpSearchRequestDto input)
        {
            var result = (await base.Repository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Search), p => p.Name.Contains(input.Search) || p.RefFileInstance.Classification.Name.Contains(input.Search))
                .Select(s => ObjectMapper.Map<AbpPlugins, PluginModuleDto>(s))
                .ToList();
            return new PagedResultDto<PluginModuleDto>(result.Count(), result);
        }

        public async override Task<PluginModuleDto> CreateAsync(PluginModuleDto input)
        {
            string settingFilePath = Path.Combine(env.ContentRootPath, HomeSystemConsts.DefaultPluginConfigurationFileName);
            var jsonText = File.ReadAllText(settingFilePath);
            var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptionsDTOs>(jsonText);

            var plugins = positionOptions.plugins.ToList();

            var task = await base.CreateAsync(input);

            if (task.Disabled == false)
            {
                if (plugins.Any(w => w.Name == task.Name) == false)
                {
                    plugins.Add(new PluginsDataDTO() { Name = task.Name, Disabled = task.Disabled, PluginPath = task.PluginPath });
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

        public async override Task<PluginModuleDto> UpdateAsync(Guid id, PluginModuleDto input)
        {
            string settingFilePath = Path.Combine(env.ContentRootPath, HomeSystemConsts.DefaultPluginConfigurationFileName);
            var jsonText = File.ReadAllText(settingFilePath);
            var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptionsDTOs>(jsonText);

            var plugins = positionOptions.plugins.ToList();

            var task = await base.UpdateAsync(id, input);


            if (task.Disabled == false)
            {
                if (positionOptions.plugins.Any(a => a.Name == task.Name) == false)
                {
                    plugins.Add(new PluginsDataDTO() { Name = task.Name, Disabled = task.Disabled, PluginPath = task.PluginPath });
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
                //string unloadAssembly = _pluginsManager. AssemblyLoaderManager.FindLoadContext(removeItem.Name);

                //if (!string.IsNullOrWhiteSpace(unloadAssembly))
                //{
                //    AssemblyLoaderManager.PluginUnload(AssemblyLoaderManager.FindLoadContext(removeItem.Name));
                //}

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
                string settingFilePath = Path.Combine(env.ContentRootPath, HomeSystemConsts.DefaultPluginConfigurationFileName);
                var jsonText = File.ReadAllText(settingFilePath);
                var positionOptions = System.Text.Json.JsonSerializer.Deserialize<PluginsOptionsDTOs>(jsonText);

                var plugins = positionOptions.plugins.ToList();

                var removeItem = positionOptions.plugins.SingleOrDefault(s => s.Name == currentItem.Name);

                if (removeItem != null)
                {
                    //string unloadAssembly = AssemblyLoaderManager.FindLoadContext(removeItem.Name);

                    //if (!string.IsNullOrWhiteSpace(unloadAssembly))
                    //{
                    //    AssemblyLoaderManager.PluginUnload(AssemblyLoaderManager.FindLoadContext(removeItem.Name));
                    //}

                    plugins.Remove(removeItem);

                    if (File.Exists(removeItem.PluginPath))
                    {
                        File.Delete(removeItem.PluginPath);
                    }
                }

                File.WriteAllText(settingFilePath, System.Text.Json.JsonSerializer.Serialize(positionOptions, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true }), System.Text.Encoding.UTF8);
            }

            await base.DeleteAsync(id);
        }

    }
}

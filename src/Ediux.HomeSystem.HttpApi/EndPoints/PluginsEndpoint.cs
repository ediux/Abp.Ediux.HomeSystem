using Ediux.HomeSystem.ApplicationPluginsManager;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Ediux.HomeSystem.Models.jqDataTables;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Settings;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.EndPoints
{
    [ApiController]
    [Route("api/plugins")]
    public class PluginsEndpoint : jqDataTableEndpointBase<IApplicationPluginsManager, PluginModuleDTO, Guid, PluginModuleCreateOrUpdateDTO, PluginModuleCreateOrUpdateDTO>
    {
        private readonly ICurrentUser currentUser;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IGuidGenerator guidGenerator;

        public PluginsEndpoint(IApplicationPluginsManager pluginManager,
            ICurrentUser currentUser,
            IWebHostEnvironment hostEnvironment,
            IGuidGenerator guidGenerator) : base(pluginManager)
        {       
            this.currentUser = currentUser;
            this.hostEnvironment = hostEnvironment;
            this.guidGenerator = guidGenerator;
        }

        public async override Task<IActionResult> Update([FromForm] PluginModuleCreateOrUpdateDTO input)
        {
            input.HostEnvironment = hostEnvironment;

            try
            {                
                return await base.Update(input);
            }
            catch (Exception ex)
            {
                return BadRequest(new jqDataTableResponse<PluginModuleDTO>(null, null) { error = ex.Message });
            }
         
        }

        public async override Task<IActionResult> Create([Dependency][FromForm] PluginModuleCreateOrUpdateDTO input)
        {
            input.Id = guidGenerator.Create();
            input.Disabled = true;
            input.HostEnvironment = hostEnvironment;
            input.CreationTime = DateTime.UtcNow;
            input.CreatorId = currentUser.Id;

            try
            {
                var actionResult = await base.Create(input);
                input.SaveToDisk();
                return actionResult;
            }
            catch (Exception ex)
            {                
                return BadRequest(new jqDataTableResponse<PluginModuleDTO>(null, null) { error = ex.Message });
            }

        }

        
        //[HttpPost]
        //[Route("list")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async override Task<OkObjectResult> Get([FromBody] jqDataTableRequest model)
        //{
        //    var plugins = await pluginManager.GetListAsync(new Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto());

        //    //List<PluginModuleModel> plugins = getData(model);

        //    return Task.FromResult(Ok(new jqDataTableResponse<PluginModuleDTO>(model, plugins)));
        //}

        //private List<PluginModuleModel> getData(jqDataTableRequest model)
        //{
        //    var plugins = (from q in pluginManager.PluginLoader.PluginsGetLoaded().Values
        //                   from x in q.Plugins
        //                   orderby q.Order, x.Order
        //                   select new PluginModuleModel
        //                   {
        //                       Id = q.Module + "/" + x.Name,
        //                       Module = q.Module,
        //                       Name = x.Name,
        //                       Order = x.Order,
        //                       IsBuildIn = x.IsBuildIn,
        //                       PluginPath = x.PluginPath,
        //                       Version = x.Version,
        //                       Disabled = !x.PluginSetting.Disabled
        //                   }).ToList();

        //    if (!string.IsNullOrWhiteSpace(model.search.value))
        //    {
        //        plugins = plugins.Where(w => w.Module.Contains(model.search.value) ||
        //        w.Name.Contains(model.search.value) ||
        //        w.PluginPath.Contains(model.search.value))
        //            .ToList();
        //    }

        //    return plugins;
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public override Task<IActionResult> Create([FromForm] PluginModuleModel model)
        //{
        //    try
        //    {
        //        if (model.AssemblyFile != null)
        //        {
        //            model.PluginPath = Path.Combine(pluginSettings.PluginSearchPath, model.AssemblyFile.FileName);
        //            model.Name = Path.GetFileName(model.AssemblyFile.FileName);

        //            byte[] asmBuffer = new byte[model.AssemblyFile.Length];

        //            using (Stream fs = model.AssemblyFile.OpenReadStream())
        //            {
        //                using (BinaryReader sourceRedaer = new BinaryReader(fs))
        //                {
        //                    using (BinaryWriter targetWriter = new BinaryWriter(System.IO.File.Create(model.PluginPath)))
        //                    {
        //                        long seekpos = 0L;
        //                        while (seekpos < sourceRedaer.BaseStream.Length)
        //                        {
        //                            byte[] buffered = sourceRedaer.ReadBytes(4000);

        //                            Array.Copy(buffered, 0, asmBuffer, seekpos, buffered.Length);
        //                            seekpos += buffered.Length;
        //                        }

        //                        targetWriter.Write(asmBuffer);
        //                        targetWriter.Close();

        //                        sourceRedaer.Close();
        //                    }
        //                }
        //            }

        //            pluginManager.PluginLoader.PluginLoad(new FileInfo(model.PluginPath), pluginManager.PluginLoader.PluginSettings.CreateLocalCopy);

        //        }

        //        PluginSetting pluginSetting = pluginManager.PluginLoader.PluginSettings.Plugins.SingleOrDefault(s => s.Name == model.AssemblyFile.FileName);

        //        if (pluginSetting != null)
        //        {
        //            pluginSetting.Disabled = !model.Disabled;
        //        }

        //        return Task.FromResult<IActionResult>(CreatedAtAction("Get", new { id = model.Id }, model));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromResult<IActionResult>(BadRequest(SmartError.Failed(ex.Message)));
        //    }

        //}

        //[HttpDelete]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public override Task<IActionResult> Delete([FromForm] PluginModuleModel model)
        //{
        //    try
        //    {
        //        if (model != null && !string.IsNullOrWhiteSpace(model.PluginPath))
        //        {
        //            if (model.IsBuildIn)
        //            {
        //                return Task.FromResult<IActionResult>(BadRequest(SmartError.Failed($"內建功能模組 '{model.Module}' 不能夠被卸載.")));
        //            }

        //            if (pluginManager.PluginUnload(model.Module) == false)
        //            {
        //                return Task.FromResult<IActionResult>(BadRequest(SmartError.Failed($"Assembly {model.Module} unloaded failed.")));
        //            }

        //            List<PluginSetting> removePluginSetting = pluginManager.PluginLoader.PluginSettings.Plugins.Where(w => w.Name == model.Module).ToList();

        //            if (removePluginSetting.Any())
        //            {
        //                foreach (var setting in removePluginSetting)
        //                {
        //                    try
        //                    {
        //                        string dllPath = Path.Combine(pluginManager.PluginLoader.PluginSettings.PluginSearchPath, setting.Name);

        //                        if (System.IO.File.Exists(dllPath))
        //                        {
        //                            System.IO.File.Delete(dllPath);
        //                        }

        //                        pluginManager.PluginLoader.PluginSettings.Plugins.Remove(setting);
        //                    }
        //                    catch
        //                    {
        //                        continue;
        //                    }

        //                }

        //                this.settingsService.SaveSettings<PluginSettings>(pluginManager.PluginLoader.Configuration.ConfigFileName, pluginManager.PluginLoader.PluginSettings);
        //            }

        //        }

        //        return Task.FromResult<IActionResult>(NoContent());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromResult<IActionResult>(BadRequest(SmartError.Failed(ex.Message)));
        //    }

        //}

        //public override Task<IActionResult> Update([FromForm] PluginModuleModel model)
        //{
        //    try
        //    {
        //        PluginSetting currentSetting = pluginSettings.Plugins.Where(w => w.Name == model.Module).SingleOrDefault();

        //        if (currentSetting == null)
        //        {
        //            return Task.FromResult<IActionResult>(BadRequest(SmartError.Failed($"組件'{model.Module}'註冊資訊已經遺失!可能已經遭到其他人員刪除!")));
        //        }
        //        string localPath = Path.Combine(pluginSettings.PluginSearchPath, model.AssemblyFile.FileName);

        //        CollectibleAssemblyLoadContext loadContext;
        //        Assembly curentAssembly = pluginManager.PluginLoader.LoadAssembly(localPath, out loadContext);
        //        PluginModule updatedModule = new PluginModule(curentAssembly, model.Module, new List<IPlugin>());

        //        string lastVersion = "0.0.0.0";
        //        string currentName = string.Empty;

        //        if (pluginManager.PluginLoaded(model.Module, out lastVersion, out currentName))
        //        {
        //            if (new Version(lastVersion) > (new Version(updatedModule.FileVersion)))
        //            {
        //                return Task.FromResult<IActionResult>(BadRequest(SmartError.Failed("無法更新擴充組件!因為伺服器上已經有更新版本!")));
        //            }
        //        }
        //        IPluginModule currentModeul = pluginManager.PluginLoader.PluginsGetLoaded()[model.Module];

        //        if (model.AssemblyFile != null)
        //        {
        //            if (currentModeul.Context != null)
        //            {
        //                pluginManager.PluginLoader.PluginUnload(currentModeul.Assembly);
        //            }

        //            if (System.IO.File.Exists(localPath))
        //            {
        //                System.IO.File.Delete(localPath);
        //            }

        //            model.PluginPath = localPath;
        //            model.Name = Path.GetFileName(model.AssemblyFile.FileName);

        //            byte[] asmBuffer = new byte[model.AssemblyFile.Length];

        //            using (Stream fs = model.AssemblyFile.OpenReadStream())
        //            {
        //                using (BinaryReader sourceRedaer = new BinaryReader(fs))
        //                {
        //                    using (BinaryWriter targetWriter = new BinaryWriter(System.IO.File.Create(model.PluginPath)))
        //                    {
        //                        long seekpos = 0L;
        //                        while (seekpos < sourceRedaer.BaseStream.Length)
        //                        {
        //                            byte[] buffered = sourceRedaer.ReadBytes(4000);

        //                            Array.Copy(buffered, 0, asmBuffer, seekpos, buffered.Length);
        //                            seekpos += buffered.Length;
        //                        }

        //                        targetWriter.Write(asmBuffer);
        //                        targetWriter.Close();

        //                        sourceRedaer.Close();
        //                    }
        //                }
        //            }

        //            pluginManager.PluginLoader.PluginLoad(new FileInfo(model.PluginPath), pluginManager.PluginLoader.PluginSettings.CreateLocalCopy);
        //            currentSetting = pluginSettings.Plugins.Where(w => w.Name == model.Module).SingleOrDefault();

        //        }

        //        if (currentSetting != null)
        //        {
        //            if (currentSetting.Disabled != (!model.Disabled) && currentModeul.Plugins.Any(a => a.IsBuildIn) == false)
        //            {
        //                currentSetting.Disabled = !model.Disabled;
        //                settingsService.SaveSettings(pluginManager.PluginLoader.Configuration.ConfigFileName, pluginSettings);
        //            }
        //        }

        //        return Task.FromResult<IActionResult>(NoContent());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromResult<IActionResult>(BadRequest(SmartError.Failed(ex.Message)));
        //    }
        //}
        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> Update([FromForm] IdentityRole model)
        //{
        //    var result = await _manager.UpdateAsync(model);

        //    if (result.Succeeded)
        //    {
        //        return NoContent();
        //    }

        //    return BadRequest(result);
        //}
    }
}

using Ediux.HomeSystem.ApplicationPluginsManager;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Ediux.HomeSystem.Models.jqDataTables;
using Ediux.HomeSystem.Permissions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using System;
using System.IO;
using System.Threading.Tasks;

using Volo.Abp.DependencyInjection;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Guids;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.EndPoints
{
    [ApiController]
    [Authorize(HomeSystemPermissions.PluginsManager.Execute)]
    [Route("api/plugins")]
    public class PluginsEndpoint : jqDataTableEndpointBase<IApplicationPluginsManager, PluginModuleDTO, Guid, PluginModuleCreateOrUpdateDTO, PluginModuleCreateOrUpdateDTO>
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IGuidGenerator guidGenerator;

        public PluginsEndpoint(IApplicationPluginsManager pluginManager,
            IWebHostEnvironment hostEnvironment,
            IGuidGenerator guidGenerator) : base(pluginManager)
        {
            this.hostEnvironment = hostEnvironment;
            this.guidGenerator = guidGenerator;
        }

        public async override Task<IActionResult> Update([FromForm] PluginModuleCreateOrUpdateDTO input)
        {
            input.HostEnvironment = hostEnvironment;

            try
            {
                input.ScanAndMoveFile();

                return await base.Update(input);
            }
            catch (Exception ex)
            {
                return BadRequest(new jqDataTableResponse<PluginModuleDTO>(null, null) { error = ex.Message });
            }

        }

        public override Task<IActionResult> Delete([FromForm] PluginModuleDTO input)
        {
            var result= base.Delete(input);
            input.DeleteFromDisk(hostEnvironment.ContentRootPath);
            return result;
        }

        public async override Task<IActionResult> Create([Dependency][FromForm] PluginModuleCreateOrUpdateDTO input)
        {
            input.Id = guidGenerator.Create();
            input.Disabled = true;
            input.HostEnvironment = hostEnvironment;
            //input.CreationTime = DateTime.UtcNow;
            //input.CreatorId = CurrentUser.Id;

            try
            {
                if (input. AssemblyFile != null)
                {
                    string pluginFolderPath = input.Disabled ? Path.Combine(hostEnvironment.ContentRootPath, "DisabledPlugins") : Path.Combine(hostEnvironment.ContentRootPath, "Plugins");

                    input.PluginPath = Path.Combine(pluginFolderPath, (input.AssemblyFile.FileName ?? input.AssemblyFile.Name) ?? Path.GetRandomFileName());
                    input.Name = Path.GetFileNameWithoutExtension(input.PluginPath);
                }

                var actionResult = await base.Create(input);
                input.SaveToDisk();
                return actionResult;
            }
            catch (Exception ex)
            {

                return BadRequest(new jqDataTableResponse<PluginModuleDTO>(null, null) { error = ex.Message });
            }

        }
    }
}

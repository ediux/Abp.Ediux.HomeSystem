
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Ediux.HomeSystem.Models.jqDataTables;

using System;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.ApplicationPluginsManager
{
    public interface IApplicationPluginsManager : ICrudAppService<PluginModuleDTO,  Guid>, ITransientDependency
    {
        //Task<PluginModuleDTO> CreatePluginAsync(PluginModuleDTO newModule);


    }
}

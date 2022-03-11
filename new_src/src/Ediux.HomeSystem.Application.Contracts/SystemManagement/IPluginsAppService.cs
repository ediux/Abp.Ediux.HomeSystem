
using System;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    public interface IPluginsAppService : ICrudAppService<PluginModuleDTO, Guid, AbpSearchRequestDto>, ISingletonDependency
    {
    }
}

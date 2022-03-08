
using System;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    public interface IApplicationPluginsManager : ICrudAppService<PluginModuleDTO, Guid, PluginModuleRequestDto>, ISingletonDependency
    {
    }
}

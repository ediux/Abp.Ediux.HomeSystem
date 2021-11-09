﻿
using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.PluginModule;

using System;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.ApplicationPluginsManager
{
    public interface IApplicationPluginsManager : ICrudAppService<PluginModuleDTO, Guid, jqDTSearchedResultRequestDto>, ISingletonDependency
    {
    }
}

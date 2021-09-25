using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Ediux.HomeSystem.Models.jqDataTables;

using System;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.ApplicationPluginsManager
{
    public class ApplicationPluginsManager : CrudAppService<AbpPlugins,PluginModuleDTO,Guid>, IApplicationPluginsManager
    {
        
        private readonly ICurrentUser currentUser;

        public ApplicationPluginsManager(IRepository<AbpPlugins, Guid> repository, ICurrentUser currentUser) : base(repository)
        {            
            this.currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

  

        //public Task<PluginModuleDTO> CreatePluginAsync(PluginModuleDTO newModule)
        //{
            
        //    throw new NotImplementedException();
        //}
    }
}

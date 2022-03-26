using System;
using System.Collections.Generic;
using System.Text;
using Ediux.HomeSystem.Localization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem
{
    /* Inherit your application services from this class.
     */
    public abstract class HomeSystemAppService : ApplicationService
    {
        protected HomeSystemAppService()
        {
            LocalizationResource = typeof(HomeSystemResource);
        }
    }

    public abstract class HomeSystemCrudAppService<TEntity, TEntityDto, TKey, TGetListInput> : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {

        protected HomeSystemCrudAppService(IRepository<TEntity, TKey> repository) : base(repository)
        {
            
            LocalizationResource = typeof(HomeSystemResource);
        }
    }
}

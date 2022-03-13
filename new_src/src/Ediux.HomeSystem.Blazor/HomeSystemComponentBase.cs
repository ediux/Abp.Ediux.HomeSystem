using Ediux.HomeSystem.Localization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.BlazoriseUI;

namespace Ediux.HomeSystem.Blazor
{
    public abstract class HomeSystemComponentBase : AbpComponentBase
    {
        protected HomeSystemComponentBase()
        {
            LocalizationResource = typeof(HomeSystemResource);
        }
    }

    public abstract class HomeSystemCrudPageBase<TAppService, TEntityDto, TKey, TGetListInput, TCreateInput> : AbpCrudPageBase<TAppService, TEntityDto, TKey, TGetListInput, TCreateInput>
        where TAppService : ICrudAppService<TEntityDto, TKey, TGetListInput, TCreateInput>
        where TEntityDto : IEntityDto<TKey>
        where TGetListInput : new()
        where TCreateInput : class, new()
    {
        protected HomeSystemCrudPageBase()
        {
            LocalizationResource = typeof(HomeSystemResource);
        }
    }
}

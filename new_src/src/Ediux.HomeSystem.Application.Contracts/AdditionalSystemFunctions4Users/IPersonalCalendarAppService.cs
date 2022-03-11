
using System;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public interface IPersonalCalendarAppService : ICrudAppService<PersonalCalendarDto, Guid, AbpSearchRequestDto>, ITransientDependency
    {
        Task<PagedResultDto<PersonalCalendarDto>> GetListBAsync(PersonalCalendarRequestDto input);

        Task<ListResultDto<PersonalCalendarDto>> GetRemindAsync(PersonalCalendarRequestDto input);
    }
}

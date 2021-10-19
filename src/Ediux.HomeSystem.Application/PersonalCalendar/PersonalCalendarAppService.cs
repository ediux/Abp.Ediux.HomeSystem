using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.PersonalCalendar
{
    public class PersonalCalendarAppService : CrudAppService<Data.PersonalCalendar, PersonalCalendarItemDTO, Guid, jqDTSearchedResultRequestDto>, IPersonalCalendarAppService
    {
        public PersonalCalendarAppService(IRepository<Data.PersonalCalendar, Guid> repository) : base(repository)
        {
        }

        public async override Task<PagedResultDto<PersonalCalendarItemDTO>> GetListAsync(jqDTSearchedResultRequestDto input)
        {
            var result = (await base.Repository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(input.Search), p => p.title.Contains(input.Search) || p.description.Contains(input.Search))
                .Where(w => w.CreatorId == CurrentUser.Id)
                .ToList();

            return new PagedResultDto<PersonalCalendarItemDTO>(result.Count(), await MapToGetListOutputDtosAsync(result));
        }

        public async Task<PagedResultDto<PersonalCalendarItemDTO>> GetListAsync(PersonalCalendarRequestDTO input)
        {
            var result = await MapToGetListOutputDtosAsync((await base.Repository.GetQueryableAsync())
               .Where(w => w.CreatorId == CurrentUser.Id)
               .ToList());

            result = result.WhereIf(input.Start.HasValue, p => p.StartTime >= input.Start)
                .WhereIf(input.End.HasValue, p => p.EndTime <= input.End)
                .ToList();

            return new PagedResultDto<PersonalCalendarItemDTO>(result.Count(), result);
        }
    }
}

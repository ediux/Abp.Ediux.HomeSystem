using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<PagedResultDto<PersonalCalendarItemDTO>> GetListAsync(jqDT_PersonalCalendarResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        public override Task<PagedResultDto<PersonalCalendarItemDTO>> GetListAsync(jqDTSearchedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }
    }
}

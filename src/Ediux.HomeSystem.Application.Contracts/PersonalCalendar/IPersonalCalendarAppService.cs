using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.PersonalCalendar
{
    public interface IPersonalCalendarAppService : ICrudAppService<PersonalCalendarItemDTO, Guid, jqDTSearchedResultRequestDto>, ITransientDependency
    {
        Task<PagedResultDto<PersonalCalendarItemDTO>> GetListBAsync(PersonalCalendarRequestDTO input);
    }
}

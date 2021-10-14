using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;
using Ediux.HomeSystem.PersonalCalendar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.EndPoints
{
    public class PersonalCalendarEndPoint : jqDataTableEndpointBase<IPersonalCalendarAppService, PersonalCalendarItemDTO, Guid, PersonalCalendarItemDTO, PersonalCalendarItemDTO>
    {
        public PersonalCalendarEndPoint(IPersonalCalendarAppService crudAppService) : base(crudAppService)
        {
        }
    }
}

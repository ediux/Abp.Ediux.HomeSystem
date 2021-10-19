using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;
using Ediux.HomeSystem.Models.PersonalCalendar;
using Ediux.HomeSystem.PersonalCalendar;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Controllers
{
    [ApiController]
    [Authorize(Permissions.HomeSystemPermissions.PersonalCalendar.Execute)]
    [Route("api/calendars/events")]
    public class CalendarsController : HomeSystemController
    {
        private IPersonalCalendarAppService crudAppService;
        public CalendarsController(IPersonalCalendarAppService crudAppService)
        {
            this.crudAppService = crudAppService;
        }

        [HttpGet]
        public async Task<ActionResult> Index([FromQuery] string start, [FromQuery] string end)
        {
            var request = new PersonalCalendarRequestDTO(start, end);

            var result = await crudAppService.GetListAsync(request);
            return Json(ObjectMapper.Map<IReadOnlyList<PersonalCalendarItemDTO>, IReadOnlyList<CalendarInputViewModel>>(result.Items));
        }
    }
}

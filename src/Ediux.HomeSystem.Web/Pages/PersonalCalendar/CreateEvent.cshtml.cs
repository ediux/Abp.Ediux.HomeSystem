using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;
using Ediux.HomeSystem.Models.PersonalCalendar;
using Ediux.HomeSystem.PersonalCalendar;
using Ediux.HomeSystem.Web.Models.PersonalCalendar;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Volo.Abp.Guids;

namespace Ediux.HomeSystem.Web.Pages.PersonalCalendar
{
    public class CreateEventModel : HomeSystemPageModel
    {
        private IPersonalCalendarAppService personalCalendarAppService;
        private readonly IGuidGenerator guidGenerator;

        public CreateEventModel(IPersonalCalendarAppService personalCalendarAppService,
           IGuidGenerator guidGenerator)
        {
            this.personalCalendarAppService = personalCalendarAppService;
            this.guidGenerator = guidGenerator;
        }

        [BindProperty]
        public CalendarInputUIViewModel CalendarEvent { get; set; }

        public void OnGet()
        {
            CalendarEvent = new CalendarInputUIViewModel();
            CalendarEvent.Id = guidGenerator.Create();
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var entity = ObjectMapper.Map<CalendarInputViewModel, PersonalCalendarItemDTO>(CalendarEvent);
                entity.CreationTime = DateTime.UtcNow;
                entity.CreatorId = CurrentUser.Id;
                await personalCalendarAppService.CreateAsync(entity);
            }
        }
    }
}

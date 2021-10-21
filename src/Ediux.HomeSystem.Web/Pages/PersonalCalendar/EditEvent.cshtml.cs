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

namespace Ediux.HomeSystem.Web.Pages.PersonalCalendar
{
    public class EditEventModel : HomeSystemPageModel
    {
        private IPersonalCalendarAppService personalCalendarAppService;


        public EditEventModel(IPersonalCalendarAppService personalCalendarAppService)
        {
            this.personalCalendarAppService = personalCalendarAppService;
        }

        [BindProperty]
        public CalendarInputUIViewModel CalendarEvent { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            CalendarEvent = ObjectMapper.Map<PersonalCalendarItemDTO, CalendarInputUIViewModel>(await personalCalendarAppService.GetAsync(id));
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {                
                var entity = ObjectMapper.Map<CalendarInputViewModel, PersonalCalendarItemDTO>(CalendarEvent);
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
                await personalCalendarAppService.UpdateAsync(entity.Id, entity);
            }
        }
    }
}

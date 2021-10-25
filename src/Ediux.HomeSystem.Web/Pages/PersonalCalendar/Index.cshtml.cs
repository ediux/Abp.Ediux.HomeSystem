using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.PersonalCalendar;

using Microsoft.AspNetCore.Authorization;

using Volo.Abp.Guids;

namespace Ediux.HomeSystem.Web.Pages.PersonalCalendar
{
    [Authorize(HomeSystemPermissions.PersonalCalendar.Execute)]
    public class IndexModel : HomeSystemPageModel
    {
        private IPersonalCalendarAppService personalCalendarAppService;
        //private readonly IGuidGenerator guidGenerator;

        public IndexModel(IPersonalCalendarAppService personalCalendarAppService)
        {
            this.personalCalendarAppService = personalCalendarAppService;
            //this.guidGenerator = guidGenerator;
        }

        public void OnGet()
        {
        }

     
    }
}

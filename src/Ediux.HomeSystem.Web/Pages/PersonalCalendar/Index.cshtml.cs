using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ediux.HomeSystem.Permissions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ediux.HomeSystem.Web.Pages.PersonalCalendar
{
    [Authorize(HomeSystemPermissions.PersonalCalendar.Execute)]
    public class IndexModel : HomeSystemPageModel
    {
        public void OnGet()
        {

        }
    }
}

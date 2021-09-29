using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ediux.HomeSystem.Permissions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ediux.HomeSystem.Web.Pages
{
    [Authorize(HomeSystemPermissions.PasswordBook)]
    public class PassworkBookModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

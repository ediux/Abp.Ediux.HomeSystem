
using Ediux.HomeSystem.Permissions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ediux.HomeSystem.Web.Pages.PasswordBook
{
    [Authorize(HomeSystemPermissions.PasswordBook.Execute)]
    public class IndexModel :  HomeSystemPageModel
    {
        public void OnGet()
        {
        }
    }
}

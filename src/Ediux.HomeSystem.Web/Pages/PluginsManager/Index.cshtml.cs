
using Ediux.HomeSystem.Permissions;

using Microsoft.AspNetCore.Authorization;

namespace Ediux.HomeSystem.Web.Pages.PluginsManager
{
    [Authorize(HomeSystemPermissions.PluginsManager.Execute)]
    public class IndexModel : HomeSystemPageModel
    {
        public void OnGet()
        {
        }
    }
}


using Microsoft.AspNetCore.Authorization;

using Volo.Abp.FeatureManagement;

namespace Ediux.HomeSystem.Web.Pages
{
    [Authorize(policy: FeatureManagementPermissions.ManageHostFeatures)]
    public class PluginsManagerModel : HomeSystemPageModel
    {
        public void OnGet()
        {
        }
    }
}

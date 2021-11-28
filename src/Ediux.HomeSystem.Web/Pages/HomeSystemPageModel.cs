using Ediux.HomeSystem.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ediux.HomeSystem.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class HomeSystemPageModel : AbpPageModel
    {
      

        protected HomeSystemPageModel()
        {
            LocalizationResourceType = typeof(HomeSystemResource);
        }
    }
}
    
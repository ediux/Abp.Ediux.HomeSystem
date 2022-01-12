using Ediux.HomeSystem.Plugins.HololivePages.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ediux.HomeSystem.Plugins.HololivePages.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class HololivePagesPageModel : AbpPageModel
    {
        protected HololivePagesPageModel()
        {
            LocalizationResourceType = typeof(HololivePagesResource);
            ObjectMapperContext = typeof(HololivePagesWebModule);
        }
    }
}
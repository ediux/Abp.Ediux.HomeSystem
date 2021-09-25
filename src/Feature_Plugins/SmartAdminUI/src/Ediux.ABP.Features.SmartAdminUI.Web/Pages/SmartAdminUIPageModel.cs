using Ediux.ABP.Features.SmartAdminUI.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ediux.ABP.Features.SmartAdminUI.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class SmartAdminUIPageModel : AbpPageModel
    {
        protected SmartAdminUIPageModel()
        {
            LocalizationResourceType = typeof(SmartAdminUIResource);
            ObjectMapperContext = typeof(SmartAdminUIWebModule);
        }
    }
}
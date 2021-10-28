using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.CmsKit.Localization;

namespace Ediux.HomeSystem.Web.Pages.CmsKit.Admins
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class CmsKitAdminPageModel : AbpPageModel
    {
        protected CmsKitAdminPageModel()
        {
            LocalizationResourceType = typeof(CmsKitResource);
            //ObjectMapperContext = typeof(HomeSystemWebModule);
        }
    }
}

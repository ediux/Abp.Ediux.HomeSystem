using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.CmsKit.Localization;

namespace Ediux.HomeSystem.Web.Pages.CmsKit
{
    public abstract class CommonPageModel : AbpPageModel
    {
        protected CommonPageModel()
        {
            LocalizationResourceType = typeof(CmsKitResource);
        }
    }
}
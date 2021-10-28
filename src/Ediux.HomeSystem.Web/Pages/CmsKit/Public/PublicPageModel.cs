using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.CmsKit.Localization;

namespace Ediux.HomeSystem.Web.Pages.CmsKit.Public
{
    public abstract class CmsKitPublicPageModelBase : AbpPageModel
    {
        protected CmsKitPublicPageModelBase()
        {
            LocalizationResourceType = typeof(CmsKitResource);
        }
    }
}

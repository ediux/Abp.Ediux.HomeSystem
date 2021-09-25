using Ediux.ABP.Features.SmartAdminUI.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ediux.ABP.Features.SmartAdminUI.Pages
{
    public abstract class SmartAdminUIPageModel : AbpPageModel
    {
        protected SmartAdminUIPageModel()
        {
            LocalizationResourceType = typeof(SmartAdminUIResource);
        }
    }
}
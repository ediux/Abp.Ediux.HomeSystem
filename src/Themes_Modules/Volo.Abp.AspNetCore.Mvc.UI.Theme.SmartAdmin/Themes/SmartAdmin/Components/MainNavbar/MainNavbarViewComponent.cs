using Microsoft.AspNetCore.Mvc;

namespace Volo.Abp.AspNetCore.Mvc.UI.Theme.SmartAdmin.Themes.Basic.Components.MainNavbar
{
    public class MainNavbarViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Themes/Basic/Components/MainNavbar/Default.cshtml");
        }
    }
}

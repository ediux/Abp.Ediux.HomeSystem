using Ediux.HomeSystem.Localization;

using Microsoft.AspNetCore.Mvc;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace Ediux.HomeSystem.Web.Pages.Components.ABPHelpWidget
{
    [Widget(DisplayName = HomeSystemResource.Widgets.ABPHelpWidget, DisplayNameResource = typeof(HomeSystemResource))]
    public class ABPHelpWidgetViewComponent : AbpViewComponent
    {
        public ABPHelpWidgetViewComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

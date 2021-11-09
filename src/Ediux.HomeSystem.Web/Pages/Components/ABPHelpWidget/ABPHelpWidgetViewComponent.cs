using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.SettingManagement;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace Ediux.HomeSystem.Web.Pages.Components.ABPHelpWidget
{
    [Widget(DisplayName = HomeSystemResource.Widgets.ABPHelpWidget, DisplayNameResource = typeof(HomeSystemResource))]
    public class ABPHelpWidgetViewComponent : AbpViewComponent
    {
        private readonly IWebSiteSettingsAppService webSiteSettingsAppService;
        public ABPHelpWidgetViewComponent(IWebSiteSettingsAppService webSiteSettingsAppService)
        {
            this.webSiteSettingsAppService = webSiteSettingsAppService;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

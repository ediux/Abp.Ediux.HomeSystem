using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.Web.Components.DashboardWidgetSettingsGroup
{
    public class DashboardWidgetGroupComponents : AbpViewComponent
    {
        private ISettingManager settingManager;

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public DashBoardWidgetOption myWigets { get; set; }


        public DashboardWidgetGroupComponents(ISettingManager settingManager)
        {
            this.settingManager = settingManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           

            return View("~/Components/WebSettingsGroup/Default.cshtml");
        }
    }
}

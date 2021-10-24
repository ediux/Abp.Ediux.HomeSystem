using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.Web.Pages.Components.WelcomeWidget
{
    [Widget]
    public class WelcomeWidgetViewComponent : AbpViewComponent
    {
        protected ISettingManager SettingManager { get; }

        public WelcomeWidgetViewComponent(ISettingManager SettingManager)
        {
            this.SettingManager = SettingManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new SystemSettingsDTO() { WelcomeSlogan = (await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.WelcomeSlogan)) ?? string.Empty });
        }
    }
}

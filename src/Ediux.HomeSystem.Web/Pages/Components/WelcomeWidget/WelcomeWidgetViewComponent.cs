using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.SettingManagement;
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
    [Widget(DisplayName = HomeSystemResource.Widgets.WelcomeSloganWidget, DisplayNameResource = typeof(HomeSystemResource))]
    public class WelcomeWidgetViewComponent : AbpViewComponent
    {
        protected ISettingManager SettingManager { get; }
        protected readonly IWebSiteSettingsAppService webSiteSettingsAppService;
        public WelcomeWidgetViewComponent(ISettingManager SettingManager, IWebSiteSettingsAppService webSiteSettingsAppService)
        {
            this.SettingManager = SettingManager;
            this.webSiteSettingsAppService = webSiteSettingsAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new SystemSettingsDTO() { WelcomeSlogan = (await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.WelcomeSlogan)) ?? string.Empty });
        }
    }
}

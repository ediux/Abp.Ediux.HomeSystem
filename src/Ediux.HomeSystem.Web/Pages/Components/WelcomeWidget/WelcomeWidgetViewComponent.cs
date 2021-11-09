using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.SettingManagement;
using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.Web.Pages.Components.WelcomeWidget
{
    [Widget(DisplayName = HomeSystemResource.Widgets.WelcomeSloganWidget, DisplayNameResource = typeof(HomeSystemResource))]
    public class WelcomeWidgetViewComponent : AbpViewComponent
    {
        protected readonly ISettingManagementAppService settingManagementAppService;

        public WelcomeWidgetViewComponent(ISettingManagementAppService  settingManagementAppService)
        {
            this.settingManagementAppService = settingManagementAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new SystemSettingsDTO() { WelcomeSlogan = (await settingManagementAppService.GetOrNullGlobalAsync(HomeSystemSettings.WelcomeSlogan)) ?? string.Empty });
        }
    }
}

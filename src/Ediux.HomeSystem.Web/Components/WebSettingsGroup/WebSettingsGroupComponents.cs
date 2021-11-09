using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.SettingManagement;
using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Components.WebSettingsGroup
{
    [Authorize(HomeSystemPermissions.Settings.Prefix)]
    public class WebSettingsGroupComponents : AbpViewComponent
    {
        private readonly ISettingManagementAppService settingManagementAppService;
        public WebSettingsGroupComponents(ISettingManagementAppService settingManagementAppService)
        {
            ObjectMapperContext = typeof(HomeSystemWebModule);
            this.settingManagementAppService = settingManagementAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SystemSettingsDTO model = new SystemSettingsDTO();
            model.WebSite = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.SiteName);
            model.WelcomeSlogan = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.WelcomeSlogan);
            return View("~/Components/WebSettingsGroup/Default.cshtml", model);
        }
    }
}

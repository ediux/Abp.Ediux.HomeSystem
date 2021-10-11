using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.SettingManagement
{
    [Authorize(HomeSystemPermissions.Settings.Preifx)]
    public class WebSiteSettingsAppService : ApplicationService, IWebSiteSettingsAppService
    {
        protected ISettingManager SettingManager { get; }

        public WebSiteSettingsAppService(ISettingManager settingManager)
        {
            ObjectMapperContext = typeof(HomeSystemApplicationModule);
            LocalizationResource = typeof(HomeSystemResource);
            SettingManager = settingManager;
        }

        public async Task<SystemSettingsDTO> GetAsync()
        {
            return new SystemSettingsDTO() { WebSite = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.SiteName) };
        }

        public async Task UpdateAsync(SystemSettingsDTO input)
        {
            await SettingManager.SetGlobalAsync(HomeSystemSettings.SiteName, input.WebSite);
        }
    }
}

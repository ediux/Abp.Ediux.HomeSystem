using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.Settings;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.SettingManagement
{

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
            return new SystemSettingsDTO()
            {
                WebSite = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.SiteName),
                WelcomeSlogan = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.WelcomeSlogan)
            };
        }

        public async Task UpdateAsync(SystemSettingsDTO input)
        {
            await SettingManager.SetGlobalAsync(HomeSystemSettings.SiteName, input.WebSite);
            await SettingManager.SetGlobalAsync(HomeSystemSettings.WelcomeSlogan, input.WelcomeSlogan);
        }

        public async Task UpdateDashboardGlobalAsync(DashboardWidgetRequestedDTOs input)
        {
            if (input != null)
            {
                string availableDashBoardWidgets = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets);
                DashBoardWidgetOptionDTOs myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));

                if (input.SelectedDefaultWidgets != null && input.SelectedDefaultWidgets.Any())
                {
                    myWigets.Widgets.ToList().ForEach(s => { s.Default = false; });

                    foreach (var search in input.SelectedDefaultWidgets)
                    {
                        var widget = myWigets.Widgets.SingleOrDefault(a => a.Name == search);

                        if (widget != null)
                        {
                            widget.Default = true;
                        }
                    }

                    await SettingManager.SetGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets, JsonSerializer.Serialize(myWigets));
                }
            }
        }

        public async Task<List<string>> GetDashboardWidgetListsAsync()
        {
            string availableDashBoardWidgets = await SettingManager.GetOrNullForCurrentUserAsync(HomeSystemSettings.AvailableDashBoardWidgets);
            DashBoardWidgetOptionDTOs myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));
            return myWigets.Widgets.Select(s => s.Name).ToList();
        }
    }
}

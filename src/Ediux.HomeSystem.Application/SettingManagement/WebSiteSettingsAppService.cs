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

using Volo.Abp;
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
                    availableDashBoardWidgets = JsonSerializer.Serialize(myWigets);
                    await SettingManager.SetGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets, availableDashBoardWidgets);
                }
            }
        }

        public async Task<List<string>> GetDashboardWidgetListsAsync()
        {
            if (CurrentUser.IsAuthenticated)
            {
                string availableDashBoardWidgets = await SettingManager.GetOrNullForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets);
                DashBoardWidgetOptionDTOs myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));
                return myWigets.Widgets.Select(s => s.Name).ToList();
            }
            else
            {
                string availableDashBoardWidgets = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets);
                DashBoardWidgetOptionDTOs myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));
                return myWigets.Widgets.Where(w=>w.Default).Select(s => s.Name).ToList();
            }
        }

        public async Task<DashBoardWidgetOptionDTOs> GetCurrentUserDashboardWidgetsAsync()
        {
            string availableDashBoardWidgets = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets);
            string currentUserAvailableWidgets = await SettingManager.GetOrNullForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets);

            if (CurrentUser.IsAuthenticated)
            {
                currentUserAvailableWidgets = await SettingManager.GetOrNullForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets);

                if (string.IsNullOrWhiteSpace(currentUserAvailableWidgets))
                {
                    currentUserAvailableWidgets = availableDashBoardWidgets;
                    await SettingManager.SetForCurrentUserAsync(
                        HomeSystemSettings.UserSettings.DashBoard_Widgets,
                        currentUserAvailableWidgets);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(currentUserAvailableWidgets))
                {
                    currentUserAvailableWidgets = availableDashBoardWidgets;
                }
            }

            DashBoardWidgetOptionDTOs myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(currentUserAvailableWidgets)));
            var filiterWidgets = myWigets.Widgets.WhereIf(CurrentUser.IsAuthenticated == false, p => p.Default == true).ToArray();
            myWigets.Widgets = filiterWidgets;
            return myWigets;
        }

        public async Task CreateComponentsAsync(string Input)
        {
            List<string> currentUserComponents = await GetComponentsAsync();

            if (currentUserComponents.Contains(Input))
            {
                throw new UserFriendlyException(L[HomeSystemResource.SpecifyDataItemNotFound, Input].Value,
                   code: HomeSystemDomainErrorCodes.SpecifyDataItemNotFound,
                   logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }

            currentUserComponents.Add(Input);
            await SettingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.Components, string.Join(",", currentUserComponents.ToArray()));

            string systemAvailableComponents = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableComponents);

            if (!string.IsNullOrWhiteSpace(systemAvailableComponents))
            {
                await SettingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.Components, systemAvailableComponents);
                var AvailableComponents = new List<string>(systemAvailableComponents.Split(','));

                if (AvailableComponents.Contains(Input) == false)
                {
                    AvailableComponents.Add(Input);
                    systemAvailableComponents = string.Join(",", AvailableComponents.ToArray());
                    await SettingManager.SetGlobalAsync(HomeSystemSettings.AvailableComponents, systemAvailableComponents);
                }
            }
        }

        public async Task<List<string>> GetComponentsAsync()
        {
            string currentUserComponents = await SettingManager.GetOrNullForCurrentUserAsync(HomeSystemSettings.UserSettings.Components);
            if (!string.IsNullOrWhiteSpace(currentUserComponents))
            {
                return new List<string>(currentUserComponents.Split(','));
            }
            else
            {
                string systemAvailableComponents = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableComponents);

                if (!string.IsNullOrWhiteSpace(systemAvailableComponents))
                {
                    await SettingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.Components, systemAvailableComponents);
                    return new List<string>(systemAvailableComponents.Split(','));
                }
            }

            return new List<string>();
        }

        public async Task RemoveComponentAsync(string input)
        {
            List<string> currentUserComponents = await GetComponentsAsync();

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new UserFriendlyException(L[HomeSystemResource.GeneralError].Value,
                  code: HomeSystemDomainErrorCodes.GeneralError,
                  innerException: new System.ArgumentNullException(nameof(input)),
                  logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }

            if (currentUserComponents.Contains(input))
            {
                currentUserComponents.Remove(input);
                await SettingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.Components, string.Join(",", currentUserComponents.ToArray()));
            }
        }

        public async Task<DashBoardWidgetOptionDTOs> GetAvailableDashBoardWidgetsAsync()
        {
            string availableDashBoardWidgets = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets);
            var widgetInSystem = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(
                new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));

            List<WidgetInformationDTO> AvailableDashBoardWidgets = new List<WidgetInformationDTO>(
                widgetInSystem.Widgets);

            return widgetInSystem;
        }

        public async Task AddDashboardWidgetToCurrentUserAsync(WidgetInformationDTO input)
        {
            DashBoardWidgetOptionDTOs currentUserDashboardWidgets = await GetCurrentUserDashboardWidgetsAsync();
            DashBoardWidgetOptionDTOs availableDashBoardWidgets = await GetAvailableDashBoardWidgetsAsync();

            if (currentUserDashboardWidgets.Widgets.Any(a => a.Name == input.Name))
            {
                throw new UserFriendlyException(L[HomeSystemResource.SpecifyDataItemNotFound, input.Name].Value,
                   code: HomeSystemDomainErrorCodes.SpecifyDataItemNotFound,
                   logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
            else
            {

                List<WidgetInformationDTO> alreadyUse = new List<WidgetInformationDTO>(currentUserDashboardWidgets.Widgets);
                string addWidgetName = input.Name;
                input = availableDashBoardWidgets.Widgets.SingleOrDefault(a => a.Name == addWidgetName);
                if (input != null)
                {
                    alreadyUse.Add(input);
                    currentUserDashboardWidgets.Widgets = alreadyUse.ToArray();
                    await SettingManager.SetForCurrentUserAsync(
                        HomeSystemSettings.UserSettings.DashBoard_Widgets,
                        JsonSerializer.Serialize(currentUserDashboardWidgets));
                    return;
                }

                throw new UserFriendlyException(L[HomeSystemResource.SpecifyDataItemNotFound, input.Name].Value,
                    code: HomeSystemDomainErrorCodes.SpecifyDataItemNotFound,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
        }

        public async Task RemoveDashboardWidgetFromCurrentUserAasync(WidgetInformationDTO input)
        {
            DashBoardWidgetOptionDTOs currentUserDashboardWidgets = await GetCurrentUserDashboardWidgetsAsync();
            List<WidgetInformationDTO> alreadyUse = new List<WidgetInformationDTO>(currentUserDashboardWidgets.Widgets);
            WidgetInformationDTO widget = alreadyUse.Find(a => a.Name == input.Name);
            if (widget != null)
            {
                alreadyUse.Remove(widget);
                currentUserDashboardWidgets.Widgets = alreadyUse.ToArray();
                string saveWidgetsJson = JsonSerializer.Serialize(currentUserDashboardWidgets);
                await SettingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets, saveWidgetsJson);
                return;
            }

            throw new UserFriendlyException(L[HomeSystemResource.SpecifyDataItemNotFound, input.Name].Value,
                   code: HomeSystemDomainErrorCodes.SpecifyDataItemNotFound,
                   logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
        }
    }
}

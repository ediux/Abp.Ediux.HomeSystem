using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.Options;
using Ediux.HomeSystem.Settings;

using Microsoft.Extensions.Options;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.SettingManagement
{

    public class WebSiteSettingsAppService : HomeSystemAppService, IWebSiteSettingsAppService
    {
        protected ISettingManager SettingManager { get; }
        protected IRepository<DashboardWidgets> DashboardWidgets { get; }
        protected IRepository<DashboardWidgetUsers> DashboardWidgetUsers { get; }
        protected IRepository<ComponentsRegistration> ComponentsRegistrations { get; }

        public WebSiteSettingsAppService(ISettingManager settingManager,
            IRepository<DashboardWidgets> dashboardWidgets,
            IRepository<ComponentsRegistration> componentsRegistrations,
            IRepository<DashboardWidgetUsers> dashboardWidgetUsers)
        {
            //ObjectMapperContext = typeof(HomeSystemApplicationModule);
            LocalizationResource = typeof(HomeSystemResource);
            SettingManager = settingManager;
            DashboardWidgets = dashboardWidgets;
            ComponentsRegistrations = componentsRegistrations;
            DashboardWidgetUsers = dashboardWidgetUsers;
        }

        #region 系統設定
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
        public async Task<string> GetGlobalOrNullAsync(string name)
        {
            try
            {
                string val = await SettingManager.GetOrNullGlobalAsync(name);
                return val;
            }
            catch (System.Exception ex)
            {
                throw new UserFriendlyException("取得系統參數'"+name+"'失敗!",
                    HomeSystemDomainErrorCodes.GeneralError, 
                    innerException: ex, 
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
            
        }

        public async Task SetGlobalAsync(string name, string value)
        {
            try
            {
                await SettingManager.SetGlobalAsync(name, value);
            }
            catch (System.Exception ex)
            {
                throw new UserFriendlyException("設定系統參數'" + name + "'失敗!",
                    HomeSystemDomainErrorCodes.GeneralError,
                    innerException: ex,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
            
        }
        #endregion

        #region Dashboard 設定
        public async Task UpdateDashboardGlobalAsync(DashboardWidgetRequestedDTOs input)
        {
            if (input != null)
            {
                var selectedWidgets = (await DashboardWidgets.GetListAsync()).Where(w => input.SelectedDefaultWidgets.Contains(w.Name)).ToList();

                if (selectedWidgets.Any())
                {
                    foreach (var widget in selectedWidgets)
                    {
                        widget.IsDefault = true;
                    }

                    await DashboardWidgets.UpdateManyAsync(selectedWidgets, autoSave: true);
                }
                //string availableDashBoardWidgets = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets);
                //DashBoardWidgetOptionDTOs myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));

                //if (input.SelectedDefaultWidgets != null && input.SelectedDefaultWidgets.Any())
                //{
                //    myWigets.Widgets.ToList().ForEach(s => { s.Default = false; });

                //    foreach (var search in input.SelectedDefaultWidgets)
                //    {
                //        var widget = myWigets.Widgets.SingleOrDefault(a => a.Name == search);

                //        if (widget != null)
                //        {
                //            widget.Default = true;
                //        }
                //    }
                //    availableDashBoardWidgets = JsonSerializer.Serialize(myWigets);
                //    await SettingManager.SetGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets, availableDashBoardWidgets);
                //}
            }
        }

        public async Task<List<string>> GetDashboardWidgetListsAsync()
        {
            if (CurrentUser.IsAuthenticated)
            {
                var myWidgets = (await DashboardWidgetUsers.GetQueryableAsync())
                     .Where(w => w.Id == CurrentUser.Id)
                     .Select(s => s.DashboardWidget.Name)
                     .ToList();

                return myWidgets;
            }
            else
            {
                var myWidgets = (await DashboardWidgets.GetQueryableAsync())
                    .Where(w => w.IsDefault == true)
                    .Select(s => s.Name)
                    .ToList();
                return myWidgets;
            }
        }

        public async Task<DashBoardWidgetOptionDTOs> GetCurrentUserDashboardWidgetsAsync()
        {
            if (CurrentUser.IsAuthenticated)
            {
                var myWidgets = (await DashboardWidgetUsers.GetListAsync(p => p.Id == CurrentUser.Id, includeDetails: true))
                .Select(s => ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(s.DashboardWidget))
                .ToList();

                return new DashBoardWidgetOptionDTOs() { Widgets = myWidgets.ToArray() };
            }
            else
            {
                var myWidgets = (await DashboardWidgets.GetListAsync(p => p.IsDefault, includeDetails: true))
                   .Select(s => ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(s))
                   .ToList();

                return new DashBoardWidgetOptionDTOs() { Widgets = myWidgets.ToArray() };
            }
            //string availableDashBoardWidgets = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets);
            //string currentUserAvailableWidgets = await SettingManager.GetOrNullForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets);

            //if (CurrentUser.IsAuthenticated)
            //{
            //    currentUserAvailableWidgets = await SettingManager.GetOrNullForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets);

            //    if (string.IsNullOrWhiteSpace(currentUserAvailableWidgets))
            //    {
            //        currentUserAvailableWidgets = availableDashBoardWidgets;
            //        await SettingManager.SetForCurrentUserAsync(
            //            HomeSystemSettings.UserSettings.DashBoard_Widgets,
            //            currentUserAvailableWidgets);
            //    }
            //}
            //else
            //{
            //    if (string.IsNullOrWhiteSpace(currentUserAvailableWidgets))
            //    {
            //        currentUserAvailableWidgets = availableDashBoardWidgets;
            //    }
            //}

            //DashBoardWidgetOptionDTOs myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(currentUserAvailableWidgets)));
            //var filiterWidgets = myWigets.Widgets.WhereIf(CurrentUser.IsAuthenticated == false, p => p.Default == true).ToArray();
            //myWigets.Widgets = filiterWidgets;
            //return myWigets;
        }
        //public async Task<DashBoardWidgetOptionDTOs> GetAvailableDashBoardWidgetsAsync()
        //{
        //    string availableDashBoardWidgets = await SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets);
        //    var widgetInSystem = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(
        //        new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));

        //    List<WidgetInformationDTO> AvailableDashBoardWidgets = new List<WidgetInformationDTO>(
        //        widgetInSystem.Widgets);

        //    return widgetInSystem;
        //}

        //public async Task AddDashboardWidgetToCurrentUserAsync(WidgetInformationDTO input)
        //{
        //    DashBoardWidgetOptionDTOs currentUserDashboardWidgets = await GetCurrentUserDashboardWidgetsAsync();
        //    DashBoardWidgetOptionDTOs availableDashBoardWidgets = await GetAvailableDashBoardWidgetsAsync();

        //    if (currentUserDashboardWidgets.Widgets.Any(a => a.Name == input.Name))
        //    {
        //        throw new UserFriendlyException(L[HomeSystemResource.SpecifyDataItemAlreadyExistsError, input.Name].Value,
        //           code: HomeSystemDomainErrorCodes.SpecifyDataItemAlreadyExistsError,
        //           logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
        //    }
        //    else
        //    {
        //        List<WidgetInformationDTO> alreadyUse = new List<WidgetInformationDTO>(currentUserDashboardWidgets.Widgets);
        //        string addWidgetName = input.Name;
        //        input = availableDashBoardWidgets.Widgets.SingleOrDefault(a => a.Name == addWidgetName);
        //        if (input != null)
        //        {
        //            alreadyUse.Add(input);
        //            currentUserDashboardWidgets.Widgets = alreadyUse.ToArray();
        //            await SettingManager.SetForCurrentUserAsync(
        //                HomeSystemSettings.UserSettings.DashBoard_Widgets,
        //                JsonSerializer.Serialize(currentUserDashboardWidgets));
        //            return;
        //        }

        //        throw new UserFriendlyException(L[HomeSystemResource.SpecifyDataItemNotFound, input.Name].Value,
        //            code: HomeSystemDomainErrorCodes.SpecifyDataItemNotFound,
        //            logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
        //    }
        //}

        //public async Task RemoveDashboardWidgetFromCurrentUserAasync(WidgetInformationDTO input)
        //{
        //    DashBoardWidgetOptionDTOs currentUserDashboardWidgets = await GetCurrentUserDashboardWidgetsAsync();
        //    List<WidgetInformationDTO> alreadyUse = new List<WidgetInformationDTO>(currentUserDashboardWidgets.Widgets);
        //    WidgetInformationDTO widget = alreadyUse.Find(a => a.Name == input.Name);
        //    if (widget != null)
        //    {
        //        alreadyUse.Remove(widget);
        //        currentUserDashboardWidgets.Widgets = alreadyUse.ToArray();
        //        string saveWidgetsJson = JsonSerializer.Serialize(currentUserDashboardWidgets);
        //        await SettingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets, saveWidgetsJson);
        //        return;
        //    }

        //    throw new UserFriendlyException(L[HomeSystemResource.SpecifyDataItemNotFound, input.Name].Value,
        //           code: HomeSystemDomainErrorCodes.SpecifyDataItemNotFound,
        //           logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
        //}
        public async Task<DashBoardWidgetsDTO> WidgetRegistrationAsync(DashBoardWidgetsDTO input)
        {
            DashboardWidgets widget = await DashboardWidgets.FindAsync(p => p.Name == input.Name || p.Id == input.Id);

            if (widget == null)
            {
                return ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(
                    await DashboardWidgets.InsertAsync(ObjectMapper.Map<DashBoardWidgetsDTO, DashboardWidgets>(input), autoSave: true));
            }

            return ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(widget);
        }

        public async Task<DashBoardWidgetsDTO> UpdateWidgetRegistrationAsync(DashBoardWidgetsDTO updated)
        {
            return ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(
                await DashboardWidgets.UpdateAsync(ObjectMapper.Map<DashBoardWidgetsDTO, DashboardWidgets>(updated)));
        }

        public async Task WidgetUnRegistrationAsync(DashBoardWidgetsDTO input)
        {
            await DashboardWidgets.DeleteAsync(p => p.Id == input.Id);
        }

        public async Task AddDashboardWidgetToCurrentUserAsync(DashBoardWidgetsDTO input)
        {
            DashboardWidgetUsers user = await DashboardWidgetUsers.FindAsync(p => p.Id == CurrentUser.Id && p.DashboardWidgetId == input.Id);

            if (user == null)
            {
                user = new DashboardWidgetUsers(CurrentUser.Id.Value, input.Id);
                await DashboardWidgetUsers.InsertAsync(user);
            }
        }

        public async Task RemoveDashboardWidgetFromCurrentUserAasync(DashBoardWidgetsDTO input)
        {
            await DashboardWidgetUsers.DeleteAsync(p => p.Id == CurrentUser.Id && p.DashboardWidgetId == input.Id, autoSave: true);
        }

        public async Task<DashBoardWidgetOptionDTOs> GetAvailableDashboardWidgetsAsync()
        {
            return new DashBoardWidgetOptionDTOs()
            {
                Widgets = (await DashboardWidgets.GetListAsync(includeDetails: true))
                    .Select(s => ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(s))
                    .ToArray()
            };
        }
        #endregion

        #region 元件註冊
        public async Task CreateComponentsAsync(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input.Trim()))
            {
                throw new UserFriendlyException(L[HomeSystemResource.GeneralError].Value,
                    code: HomeSystemDomainErrorCodes.GeneralError,
                    innerException: new System.ArgumentNullException(nameof(Input)),
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }

            List<string> currentUserComponents = await GetComponentsAsync();

            if (currentUserComponents.Contains(Input) == false)
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






        #endregion

    }
}

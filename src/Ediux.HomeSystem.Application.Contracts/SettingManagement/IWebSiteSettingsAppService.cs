using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Models.DTOs.SystemSettings;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem.SettingManagement
{
    public interface IWebSiteSettingsAppService : IApplicationService
    {
        #region 系統設定
        Task<SystemSettingsDTO> GetAsync();

        Task UpdateAsync(SystemSettingsDTO input);
        #endregion

        #region Dashboard 設定
        Task UpdateDashboardGlobalAsync(DashboardWidgetRequestedDTOs input);

        Task UpdateCurrentUserDashboardWidgetsAsync(DashboardWidgetRequestedDTOs input);

        Task<DashBoardWidgetOptionDTOs> GetAvailableDashBoardWidgetsAsync();
        Task<DashBoardWidgetOptionDTOs> GetCurrentUserDashboardWidgetsAsync();

        Task<List<string>> GetDashboardWidgetListsAsync();
        #endregion

        #region 元件註冊
        Task CreateComponentsAsync(string Input);

        Task<List<string>> GetComponentsAsync();

        Task RemoveComponentAsync(string input);
        #endregion


    }
}

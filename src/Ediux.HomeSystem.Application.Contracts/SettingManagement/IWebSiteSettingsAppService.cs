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
        /// <summary>
        /// 取得系統設定
        /// </summary>
        /// <returns></returns>
        Task<SystemSettingsDTO> GetAsync();

        /// <summary>
        /// 更新系統設定
        /// </summary>
        /// <param name="input">更新後的系統設定值</param>
        /// <returns></returns>
        Task UpdateAsync(SystemSettingsDTO input);

        /// <summary>
        /// 取得全域設定值
        /// </summary>
        /// <param name="name">設定值名稱</param>
        /// <returns></returns>
        Task< string> GetGlobalOrNullAsync(string name);

        /// <summary>
        /// 設定全域設定值
        /// </summary>
        /// <param name="name">設定值名稱</param>
        /// <param name="value">設定值</param>
        /// <returns></returns>
        Task SetGlobalAsync(string name, string value);
        #endregion

        #region Dashboard 設定
        //Task<DashBoardWidgetsDTO> GetWidgetAsync()
        Task UpdateDashboardGlobalAsync(DashboardWidgetRequestedDTOs input);
        /// <summary>
        /// 註冊Widget至系統資料庫
        /// </summary>
        /// <param name="input">欲註冊的Widget資訊物件。</param>
        /// <returns>傳回新增成功的註冊資訊。</returns>
        Task<DashBoardWidgetsDTO> WidgetRegistrationAsync(DashBoardWidgetsDTO input);

        /// <summary>
        /// 更新Widget資訊至系統資料庫
        /// </summary>
        /// <param name="updated"></param>
        /// <returns></returns>
        Task<DashBoardWidgetsDTO> UpdateWidgetRegistrationAsync(DashBoardWidgetsDTO updated);

        /// <summary>
        /// 取消註冊Widget
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task WidgetUnRegistrationAsync(DashBoardWidgetsDTO input);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<DashBoardWidgetOptionDTOs> GetAvailableDashboardWidgetsAsync();

        /// <summary>
        /// 取得目前登入使用者使用中的Widget
        /// </summary>
        /// <returns></returns>
        Task<DashBoardWidgetOptionDTOs> GetCurrentUserDashboardWidgetsAsync();

        /// <summary>
        /// 將Widget設定給目前登入使用者使用
        /// </summary>
        /// <param name="input">要指派的Widget</param>
        /// <returns></returns>
        Task AddDashboardWidgetToCurrentUserAsync(DashBoardWidgetsDTO input);

        /// <summary>
        /// 移除指定Widget與目前登入使用者的關聯
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task RemoveDashboardWidgetFromCurrentUserAasync(DashBoardWidgetsDTO input);

        /// <summary>
        /// 取得目前可用的Widget清單
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetDashboardWidgetListsAsync();
        #endregion

        #region 元件註冊
        Task CreateComponentsAsync(string Input);

        Task<List<string>> GetComponentsAsync();

        Task RemoveComponentAsync(string input);
        #endregion


    }
}

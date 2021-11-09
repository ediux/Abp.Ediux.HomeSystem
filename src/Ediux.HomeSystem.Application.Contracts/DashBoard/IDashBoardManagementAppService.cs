using Ediux.HomeSystem.Models.DTOs.DashBoard;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.DashBoard
{
    public interface IDashBoardManagementAppService : IApplicationService, ITransientDependency
    {
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
    }
}

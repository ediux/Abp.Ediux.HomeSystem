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
        Task<SystemSettingsDTO> GetAsync();

        Task UpdateAsync(SystemSettingsDTO input);

        Task UpdateDashboardGlobalAsync(DashboardWidgetRequestedDTOs input);

        Task<List<string>> GetDashboardWidgetListsAsync();
    }
}

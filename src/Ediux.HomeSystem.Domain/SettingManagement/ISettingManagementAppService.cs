﻿
using Ediux.HomeSystem.Models.DTOs.FCM;
using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.SettingManagement
{
    public interface ISettingManagementAppService : ISettingsManagementSuperAppService, ISettingManager
    {
        Task UpdateFCMSettingsAsync(FCMSettingsDTO input);

        Task<FCMSettingsDTO> GetFCMSettingsAsync();

        Task<BatchSettingsDTO> GetBatchSettingsAsync();

        Task UpdateBatchSettingsAsync(BatchSettingsDTO input);
    }
}

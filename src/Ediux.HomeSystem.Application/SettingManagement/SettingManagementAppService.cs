using Ediux.HomeSystem.Models.DTOs.FCM;
using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.Settings;

using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Ediux.HomeSystem.SettingManagement
{
    public class SettingManagementAppService : SettingManager, ISettingManagementAppService, IRemoteService
    {
        public SettingManagementAppService(IOptions<SettingManagementOptions> options, IServiceProvider serviceProvider, ISettingDefinitionManager settingDefinitionManager, ISettingEncryptionService settingEncryptionService) : base(options, serviceProvider, settingDefinitionManager, settingEncryptionService)
        {

        }

        public async Task<BatchSettingsDTO> GetBatchSettingsAsync()
        {

            try
            {
                return new BatchSettingsDTO()
                {
                    Timer_Period = int.Parse(await GetGlobalOrNullAsync(HomeSystemSettings.BatchSettings.Timer_Period))
                };
            }
            catch (System.Exception ex)
            {
                throw new UserFriendlyException("取得FCM組態設定失敗!",
                    HomeSystemDomainErrorCodes.GeneralError,
                    innerException: ex,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
        }

        public async Task<FCMSettingsDTO> GetFCMSettingsAsync()
        {
            try
            {
                return new FCMSettingsDTO()
                {
                    apiKey = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.ApiKey),
                    appId = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.AppId),
                    authDomain = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.AuthDomain),
                    FCMVersion = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.FCMVersion),
                    measurementId = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.MeasurementId),
                    messagingSenderId = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.MessagingSenderId),
                    projectId = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.ProjectId),
                    serviceKey = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.ServiceKey),
                    storageBucket = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.StorageBucket),
                    vapidKey = await GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.VAPIdKey)
                };

            }
            catch (System.Exception ex)
            {
                throw new UserFriendlyException("取得FCM組態設定失敗!",
                    HomeSystemDomainErrorCodes.GeneralError,
                    innerException: ex,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
        }

        public async Task<string> GetGlobalOrNullAsync(string name)
        {
            try
            {
                string val = await GlobalSettingManagerExtensions.GetOrNullGlobalAsync(this, name);
                return val;
            }
            catch (System.Exception ex)
            {
                throw new UserFriendlyException("取得系統參數'" + name + "'失敗!",
                    HomeSystemDomainErrorCodes.GeneralError,
                    innerException: ex,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
        }

        public async Task SetGlobalAsync(string name, string value)
        {
            try
            {
                await GlobalSettingManagerExtensions.SetGlobalAsync(this, name, value);
            }
            catch (System.Exception ex)
            {
                throw new UserFriendlyException("設定系統參數'" + name + "'失敗!",
                    HomeSystemDomainErrorCodes.GeneralError,
                    innerException: ex,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
        }

        public async Task UpdateBatchSettingsAsync(BatchSettingsDTO input)
        {
            try
            {
                await GlobalSettingManagerExtensions.SetGlobalAsync(this, HomeSystemSettings.BatchSettings.Timer_Period, $"{input.Timer_Period}");
            }
            catch (System.Exception ex)
            {
                throw new UserFriendlyException("設定批次參數失敗!",
                    HomeSystemDomainErrorCodes.GeneralError,
                    innerException: ex,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
        }

        public async Task UpdateFCMSettingsAsync(FCMSettingsDTO input)
        {
            try
            {
                if (input != null)
                {
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.FCMVersion, input.FCMVersion);
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.MeasurementId, input.measurementId);
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.ProjectId, input.projectId);
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.AppId, input.appId);
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.ServiceKey, input.serviceKey);
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.MessagingSenderId, input.messagingSenderId);
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.AuthDomain, input.authDomain);
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.StorageBucket, input.storageBucket);
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.ApiKey, input.apiKey);
                    await SetGlobalAsync(HomeSystemSettings.FCMSettings.VAPIdKey, input.vapidKey);
                }
            }
            catch (System.Exception ex)
            {
                throw new UserFriendlyException("設定網站參數設定失敗!",
                    HomeSystemDomainErrorCodes.GeneralError,
                    innerException: ex,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
        }

        public async Task UpdateWebSettingAsync(SystemSettingsDTO input)
        {
            try
            {
                if (input != null)
                {
                    await SetGlobalAsync(HomeSystemSettings.SiteName, input.WebSite);
                    await SetGlobalAsync(HomeSystemSettings.WelcomeSlogan, input.WelcomeSlogan);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("設定網站參數設定失敗!",
                    HomeSystemDomainErrorCodes.GeneralError,
                    innerException: ex,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
        }
    }
}

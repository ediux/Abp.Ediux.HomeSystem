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
            catch (System.Exception ex)
            {
                throw new UserFriendlyException("設定網站參數設定失敗!",
                    HomeSystemDomainErrorCodes.GeneralError,
                    innerException: ex,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }
        }
    }
}

using Ediux.HomeSystem.Models.DTOs.FCM;
using Ediux.HomeSystem.SettingManagement;
using Ediux.HomeSystem.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Components.FCMSettingGroup
{
    
    public class FCMSettingsGroupComponents : AbpViewComponent
    {
        private readonly ISettingManagementAppService settingManagementAppService;
        public FCMSettingsGroupComponents(ISettingManagementAppService settingManagementAppService)
        {
            ObjectMapperContext = typeof(HomeSystemWebModule);
            this.settingManagementAppService = settingManagementAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            FCMSettingsDTO model = new FCMSettingsDTO();

            model.authDomain = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.AuthDomain);
            model.apiKey = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.ApiKey);
            model.appId = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.AppId);
            model.messagingSenderId = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.MessagingSenderId);
            model.measurementId = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.MeasurementId);
            model.serviceKey = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.ServiceKey);
            model.storageBucket = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.StorageBucket);
            model.projectId = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.ProjectId);
            model.FCMVersion = await settingManagementAppService.GetGlobalOrNullAsync(HomeSystemSettings.FCMSettings.FCMVersion);
            
            return View("~/Components/FCMSettingGroup/Default.cshtml", model);
        }
    }
}

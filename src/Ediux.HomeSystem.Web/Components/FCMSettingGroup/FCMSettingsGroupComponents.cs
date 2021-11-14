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
            FCMSettingsDTO model = await settingManagementAppService.GetFCMSettingsAsync();
            return View("~/Components/FCMSettingGroup/Default.cshtml", model);
        }
    }
}

using Ediux.HomeSystem.SettingManagement;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Pages.Components.Firebase
{
    public class FirebaseViewComponent : AbpViewComponent
    {
        private readonly ISettingManagementAppService settingManagementAppService;

        public FirebaseViewComponent(ISettingManagementAppService settingManagementAppService)
        {
            this.settingManagementAppService = settingManagementAppService; 
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await settingManagementAppService.GetFCMSettingsAsync();  
            return View("/Components/LayoutHooks/Firebase/Default.cshtml", model);
        }
    }
}

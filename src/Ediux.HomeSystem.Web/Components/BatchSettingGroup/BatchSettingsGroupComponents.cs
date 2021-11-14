using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.SettingManagement;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Components.BatchSettingGroup
{
    public class BatchSettingsGroupComponents : AbpViewComponent
    {
        private readonly ISettingManagementAppService settingManagementAppService;

        public BatchSettingsGroupComponents(ISettingManagementAppService settingManagementAppService)
        {
            this.settingManagementAppService = settingManagementAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            BatchSettingsDTO model = await settingManagementAppService.GetBatchSettingsAsync();

            return View("~/Components/BatchSettingGroup/Default.cshtml", model);
        }
    }
}

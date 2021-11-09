using Ediux.HomeSystem.Models.DashBoard;
using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.SettingManagement;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Components.DashboardWidgetSettingsGroup
{
    public class DashboardWidgetGroupComponents : AbpViewComponent
    {
        private IWebSiteSettingsAppService settingManager;

        [TempData]
        public string Message { get; set; }

        public DashboardWidgetGroupComponents(IWebSiteSettingsAppService settingManager)
        {
            this.settingManager = settingManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DashBoardWidgetOptionDTOs myWigets = await settingManager.GetCurrentUserDashboardWidgetsAsync();

            ChangedDefaultWidgetsViewModel output = new ChangedDefaultWidgetsViewModel();
            output.WidgetLists = myWigets.Widgets.Select(s => new SelectListItem(s.DisplayName, s.Name)).ToList();
            output.SelectedDefaultWidgets = await settingManager.GetDashboardWidgetListsAsync();
            return View("~/Components/DashboardWidgetSettingsGroup/Default.cshtml", output);
        }
    }
}

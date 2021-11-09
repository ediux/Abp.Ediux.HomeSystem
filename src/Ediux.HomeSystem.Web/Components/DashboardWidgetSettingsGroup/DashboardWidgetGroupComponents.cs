using Ediux.HomeSystem.DashBoard;
using Ediux.HomeSystem.Models.DashBoard;
using Ediux.HomeSystem.Models.DTOs.DashBoard;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Components.DashboardWidgetSettingsGroup
{
    public class DashboardWidgetGroupComponents : AbpViewComponent
    {
        private IDashBoardManagementAppService dashBoardManagementAppService;

        [TempData]
        public string Message { get; set; }

        public DashboardWidgetGroupComponents(IDashBoardManagementAppService dashBoardManagementAppService)
        {
            this.dashBoardManagementAppService = dashBoardManagementAppService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DashBoardWidgetOptionDTOs myWigets = await dashBoardManagementAppService.GetAvailableDashboardWidgetsAsync();

            ChangedDefaultWidgetsViewModel output = new ChangedDefaultWidgetsViewModel();
            output.WidgetLists = myWigets.Widgets.Select(s => new SelectListItem(s.DisplayName, s.Name)).ToList();
            output.SelectedDefaultWidgets = await dashBoardManagementAppService.GetDashboardWidgetListsAsync();
            return View("~/Components/DashboardWidgetSettingsGroup/Default.cshtml", output);
        }
    }
}

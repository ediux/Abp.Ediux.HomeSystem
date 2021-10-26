using Ediux.HomeSystem.Models.DashBoard;
using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.Web.Components.DashboardWidgetSettingsGroup
{
    public class DashboardWidgetGroupComponents : AbpViewComponent
    {
        private ISettingManager settingManager;

        [TempData]
        public string Message { get; set; }

        //[BindProperty]
        //public DashBoardWidgetOptionDTOs myWigets { get; set; }


        public DashboardWidgetGroupComponents(ISettingManager settingManager)
        {
            this.settingManager = settingManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string availableDashBoardWidgets = await settingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets);
            DashBoardWidgetOptionDTOs myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));

            ChangedDefaultWidgetsViewModel output = new ChangedDefaultWidgetsViewModel();
            output.WidgetLists = myWigets.Widgets.Select(s => new SelectListItem(s.DisplayName, s.Name)).ToList();
            output.SelectedDefaultWidgets = myWigets.Widgets.Where(w => w.Default).Select(s => s.Name).ToList();
            return View("~/Components/DashboardWidgetSettingsGroup/Default.cshtml", output);
        }
    }
}

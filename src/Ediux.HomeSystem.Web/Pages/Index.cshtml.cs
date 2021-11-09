using Ediux.HomeSystem.DashBoard;
using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Options;
using Ediux.HomeSystem.SettingManagement;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages
{
    public class IndexModel : HomeSystemPageModel
    {
        private IDashBoardManagementAppService dashBoardManagementAppService;
        private IOptions<DashboardWidgetOptions> options;

        public IndexModel(IDashBoardManagementAppService dashBoardManagementAppService, IOptions<DashboardWidgetOptions> options)
        {
            this.dashBoardManagementAppService = dashBoardManagementAppService;
            this.options = options;
            WidgetList = new List<SelectListItem>();
        }

        public List<SelectListItem> WidgetList { get; set; }

        [BindProperty]
        [DisplayName("選擇的Widget")]
        public string selectedWidget { get; set; }

        [TempData]
        public string Message { get; set; }

        public DashBoardWidgetOptionDTOs myWigets { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                DashBoardWidgetOptionDTOs widgetInSystem = await dashBoardManagementAppService.GetAvailableDashboardWidgetsAsync();

                if (widgetInSystem != null)
                {
                    if (options!=null && options.Value.Widgets.Values.Any())
                    {
                        var addAvailable = options.Value.Widgets.Values.Select(s=>s.Name).Except(widgetInSystem.Widgets.Select(s=>s.Name)).ToList();

                        if (addAvailable.Any())
                        {
                            foreach (var widgetRegister in options.Value.Widgets.Values.Where(w=>addAvailable.Contains(w.Name)).OrderBy(o=>o.Order))
                            {
                                await dashBoardManagementAppService.WidgetRegistrationAsync(widgetRegister);
                            }
                        }
                    }
                }

                myWigets = await dashBoardManagementAppService.GetCurrentUserDashboardWidgetsAsync();

                widgetInSystem = await dashBoardManagementAppService.GetAvailableDashboardWidgetsAsync();

                foreach (var item in widgetInSystem.Widgets.OrderBy(o=>o.Order))
                {
                    if (myWigets.Widgets.Any(a => a.Id == item.Id) == false)
                    {
                        WidgetList.Add(new SelectListItem(item.DisplayName, item.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                throw;
            }
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            try
            {
                DashBoardWidgetOptionDTOs widgetInSystem = await dashBoardManagementAppService.GetAvailableDashboardWidgetsAsync();
                int i = widgetInSystem.Widgets.FindIndex(o => o.Name == selectedWidget);
                await dashBoardManagementAppService.AddDashboardWidgetToCurrentUserAsync(widgetInSystem.Widgets[i]);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string name)
        {
            try
            {
                DashBoardWidgetOptionDTOs widgetInSystem = await dashBoardManagementAppService.GetAvailableDashboardWidgetsAsync();
                int i = widgetInSystem.Widgets.FindIndex(o => o.Name == name);
                await dashBoardManagementAppService.RemoveDashboardWidgetFromCurrentUserAasync(widgetInSystem.Widgets[i]);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return RedirectToPage();
        }
    }
}
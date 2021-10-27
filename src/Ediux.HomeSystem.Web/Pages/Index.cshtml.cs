using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.SettingManagement;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages
{
    public class IndexModel : HomeSystemPageModel
    {
        private IWebSiteSettingsAppService settingManager;

        public IndexModel(IWebSiteSettingsAppService settingManager)
        {
            this.settingManager = settingManager;
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
                DashBoardWidgetOptionDTOs widgetInSystem =
                await settingManager.GetAvailableDashBoardWidgetsAsync();

                if (widgetInSystem != null)
                {
                    foreach (var item in widgetInSystem.Widgets)
                    {
                        WidgetList.Add(new SelectListItem(item.DisplayName, item.Name));
                    }
                }

                myWigets = await settingManager.GetCurrentUserDashboardWidgetsAsync();
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
                await settingManager.AddDashboardWidgetToCurrentUserAsync(new WidgetInformationDTO() { Name = selectedWidget });
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
                await settingManager.RemoveDashboardWidgetFromCurrentUserAasync(new WidgetInformationDTO() { Name = name });
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return RedirectToPage();
        }
    }
}
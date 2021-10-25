using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.SettingManagement;
using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.Web.Pages
{
    public class IndexModel : HomeSystemPageModel
    {
        private ISettingManager settingManager;

        public IndexModel(ISettingManager settingManager)
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

        public DashBoardWidgetOption myWigets { get; set; }

        public async Task OnGetAsync()
        {
            await getSettingAsync();

        }

        private async Task getSettingAsync()
        {
            string availableDashBoardWidgets = await settingManager.GetOrNullGlobalAsync(HomeSystemSettings.AvailableDashBoardWidgets);
            string currentUserAvailableWidgets = await settingManager.GetOrNullGlobalAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets);

            if (CurrentUser.IsAuthenticated)
            {
                currentUserAvailableWidgets = await settingManager.GetOrNullForUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets, CurrentUser.Id.Value);

                if (string.IsNullOrWhiteSpace(currentUserAvailableWidgets))
                {
                    await settingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets, availableDashBoardWidgets);
                    currentUserAvailableWidgets = await settingManager.GetOrNullForUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets, CurrentUser.Id.Value);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(currentUserAvailableWidgets))
                {
                    await settingManager.SetGlobalAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets, availableDashBoardWidgets);
                    currentUserAvailableWidgets = await settingManager.GetOrNullGlobalAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets);
                }
            }

            var widgetInSystem = await JsonSerializer.DeserializeAsync<DashBoardWidgetOption>(new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));

            if (widgetInSystem != null)
            {
                foreach (var item in widgetInSystem.Widgets)
                {
                    WidgetList.Add(new SelectListItem(item.Name, item.Name));
                }
            }

            myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOption>(new MemoryStream(Encoding.UTF8.GetBytes(currentUserAvailableWidgets)));
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            await getSettingAsync();

            if(myWigets.Widgets.Any(a=>a.Name == selectedWidget))
            {
                Message = selectedWidget + "已經在該頁面中!";
            }
            else
            {
                myWigets.Widgets = myWigets.Widgets.Concat(new WidgetInformation[] { myWigets.Widgets.Single(a => a.Name == selectedWidget) }).ToArray();
                await settingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets, JsonSerializer.Serialize(myWigets));
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            return RedirectToPage();
        }
    }
}
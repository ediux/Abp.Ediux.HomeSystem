using Ediux.HomeSystem.Models.DTOs.DashBoard;
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
        private List<WidgetInformationDTO> AvailableDashBoardWidgets { get; set; }
        [BindProperty]
        [DisplayName("選擇的Widget")]
        public string selectedWidget { get; set; }

        [TempData]
        public string Message { get; set; }

        public DashBoardWidgetOptionDTOs myWigets { get; set; }

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

            var widgetInSystem = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(availableDashBoardWidgets)));
            AvailableDashBoardWidgets = new List<WidgetInformationDTO>(widgetInSystem.Widgets);

            if (widgetInSystem != null)
            {
                foreach (var item in widgetInSystem.Widgets)
                {
                    WidgetList.Add(new SelectListItem(item.DisplayName, item.Name));
                }
            }

            myWigets = await JsonSerializer.DeserializeAsync<DashBoardWidgetOptionDTOs>(new MemoryStream(Encoding.UTF8.GetBytes(currentUserAvailableWidgets)));
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
                List<WidgetInformationDTO> alreadyUse = new List<WidgetInformationDTO>(myWigets.Widgets);
                alreadyUse.Add(AvailableDashBoardWidgets.Single(a => a.Name == selectedWidget));
                myWigets.Widgets = alreadyUse.ToArray();
                await settingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets, JsonSerializer.Serialize(myWigets));
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string name)
        {
            await getSettingAsync();
            List<WidgetInformationDTO> alreadyUse = new List<WidgetInformationDTO>(myWigets.Widgets);
            alreadyUse.Remove(alreadyUse.Find(a => a.Name == name));
            myWigets.Widgets = alreadyUse.ToArray();
            string saveWidgetsJson = JsonSerializer.Serialize(myWigets);
            await settingManager.SetForCurrentUserAsync(HomeSystemSettings.UserSettings.DashBoard_Widgets, saveWidgetsJson);

            return RedirectToPage();
        }
    }
}
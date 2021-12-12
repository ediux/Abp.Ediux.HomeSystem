using Ediux.HomeSystem.DashBoard;
using Ediux.HomeSystem.Options;
using Ediux.HomeSystem.SettingManagement;
using Ediux.HomeSystem.Web.Models.JSONData;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.SettingManagement;
using Volo.Abp.Users;
using Volo.CmsKit.Public.Pages;

namespace Ediux.HomeSystem.Web.Pages.Components.TabViewerWidget
{
    [Widget(ScriptFiles = new[] { "/Components/Widgets/TabViewerWidget/default.js" })]
    public class TabViewerWidgetViewComponent : AbpViewComponent
    {
        protected ISettingManager SettingManager { get; }
        private readonly IDashBoardManagementAppService dashBoardManagementAppService;
        protected readonly IPagePublicAppService pagePublicAppService;
        private readonly IOptions<DashboardWidgetOptions> options;
        private readonly ICurrentUser currentUser;
        public TabViewerWidgetViewComponent(
            ISettingManager settingManager,
            IDashBoardManagementAppService dashBoardManagementAppService,
            IPagePublicAppService pagePublicAppService,
            IOptions<DashboardWidgetOptions> options,
            ICurrentUser currentUser)
        {
            SettingManager = settingManager;
            this.dashBoardManagementAppService = dashBoardManagementAppService;
            this.pagePublicAppService = pagePublicAppService;
            this.options = options;
            this.currentUser = currentUser;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Type currentType = typeof(TabViewerWidgetViewComponent);

            var widgets = await dashBoardManagementAppService.GetCurrentUserDashboardWidgetsAsync();

            if (options.Value.Widgets.ContainsKey(currentType))
            {
                string widgetGlobalSettingValue
                    = await SettingManager.GetOrNullGlobalAsync(options.Value.Widgets[currentType].GlobalSettingName) ?? options.Value.Widgets[currentType].GlobalSettingDefaultValue;

                ViewBag.TabViewerPermissionName = options.Value.Widgets[currentType].PermissionName;

                TabViewPageSetting[] tabViewPageSettings =
                           (await System.Text.Json.JsonSerializer.DeserializeAsync<TabViewPageSetting[]>(
                               new MemoryStream(System.Text.Encoding.UTF8.GetBytes(widgetGlobalSettingValue))))
                           .OrderBy(o => o.Order)
                           .ToArray();

                if (tabViewPageSettings.Any())
                {
                    foreach (TabViewPageSetting tabInfo in tabViewPageSettings)
                    {
                        tabInfo.Page = await pagePublicAppService.FindBySlugAsync(tabInfo.Slug);
                    }
                }

                return View("~/Components/Widgets/TabViewerWidget/Default.cshtml",tabViewPageSettings);
            }

            return View("~/Components/Widgets/TabViewerWidget/Default.cshtml", new TabViewPageSetting[] { });
        }
    }
}

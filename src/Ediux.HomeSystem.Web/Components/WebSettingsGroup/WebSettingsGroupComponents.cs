using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.SettingManagement;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Components.WebSettingsGroup
{
    [Authorize(HomeSystemPermissions.Settings.Prefix)]
    public class WebSettingsGroupComponents : AbpViewComponent
    {
        private readonly IWebSiteSettingsAppService _webSiteSettingsAppService;
        public WebSettingsGroupComponents(IWebSiteSettingsAppService webSiteSettingsAppService)
        {
            ObjectMapperContext = typeof(HomeSystemWebModule);
            _webSiteSettingsAppService = webSiteSettingsAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _webSiteSettingsAppService.GetAsync();

            return View("~/Components/WebSettingsGroup/Default.cshtml", model);
        }
    }
}

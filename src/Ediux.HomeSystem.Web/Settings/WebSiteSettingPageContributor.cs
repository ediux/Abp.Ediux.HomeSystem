using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.Web.Components.WebSettingsGroup;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.FeatureManagement;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;

namespace Ediux.HomeSystem.Web.Settings
{
    public class WebSiteSettingPageContributor : ISettingPageContributor
    {
        private IAuthorizationService authorizationService;

        public WebSiteSettingPageContributor(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        public async Task<bool> CheckPermissionsAsync(SettingPageCreationContext context)
        {
            bool special = await authorizationService.IsGrantedAnyAsync(HomeSystemPermissions.Settings.Special, HomeSystemPermissions.Settings.Execute);

            return special;
        }

        public async Task ConfigureAsync(SettingPageCreationContext context)
        {
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<HomeSystemResource>>();

            if(await CheckPermissionsAsync(context))
            {
                Type type = typeof(WebSettingsGroupComponents);

                context.Groups.Add(
                    new SettingPageGroup(
                        type.FullName,
                        l[HomeSystemResource.Settings.WebSettings.Prefix],
                        typeof(WebSettingsGroupComponents))
                    );
            }
        }
    }
}

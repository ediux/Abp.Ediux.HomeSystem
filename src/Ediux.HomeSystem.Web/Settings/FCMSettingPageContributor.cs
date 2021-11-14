using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.Web.Components.FCMSettingGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;

namespace Ediux.HomeSystem.Web.Settings
{
    public class FCMSettingPageContributor : ISettingPageContributor
    {
        private IAuthorizationService authorizationService;
        private IStringLocalizer<HomeSystemResource> l;

        public FCMSettingPageContributor(IStringLocalizer<HomeSystemResource> stringLocalizer, IAuthorizationService authorizationService)
        {
            this.l = stringLocalizer;
            this.authorizationService = authorizationService; 
        }

        public async Task<bool> CheckPermissionsAsync(SettingPageCreationContext context)
        {
            bool special = await authorizationService.IsGrantedAnyAsync(HomeSystemPermissions.Settings.Special, HomeSystemPermissions.Settings.Execute);
            return special;
        }

        public Task ConfigureAsync(SettingPageCreationContext context)
        {
            Type type = typeof(FCMSettingsGroupComponents);

            context.Groups.Add(
                new SettingPageGroup(
                    type.FullName,
                    l[HomeSystemResource.Settings.FCMSettings.Prefix],
                    type)
                );
            return Task.CompletedTask;
        }
    }
}

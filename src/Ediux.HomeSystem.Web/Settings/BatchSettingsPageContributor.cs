using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.Web.Components.BatchSettingGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;

namespace Ediux.HomeSystem.Web.Settings
{
    public class BatchSettingsPageContributor : ISettingPageContributor
    {
        private IAuthorizationService authorizationService;

        public BatchSettingsPageContributor(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        public async Task<bool> CheckPermissionsAsync(SettingPageCreationContext context)
        {
            bool allow = await authorizationService.IsGrantedAnyAsync(HomeSystemPermissions.Home.Options);
            return allow;
        }

        public async Task ConfigureAsync(SettingPageCreationContext context)
        {
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<HomeSystemResource>>();

            if (await CheckPermissionsAsync(context))
            {
                Type type = typeof(BatchSettingsGroupComponents);

                context.Groups.Add(
                    new SettingPageGroup(
                        type.FullName,
                        l[HomeSystemResource.Settings.BatchSettings.Prefix],
                        type)
                    );
            }
        }
    }
}

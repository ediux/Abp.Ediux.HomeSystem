using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Web.Components.FCMSettingGroup;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;

namespace Ediux.HomeSystem.Web.Settings
{
    public class FCMSettingPageContributor : ISettingPageContributor
    {
        private IStringLocalizer<HomeSystemResource> l;

        public FCMSettingPageContributor(IStringLocalizer<HomeSystemResource> stringLocalizer)
        {
            this.l = stringLocalizer;
        }

        public Task<bool> CheckPermissionsAsync(SettingPageCreationContext context)
        {
            return Task.FromResult(true);
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

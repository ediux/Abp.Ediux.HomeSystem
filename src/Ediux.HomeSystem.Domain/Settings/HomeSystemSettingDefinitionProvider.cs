using Ediux.HomeSystem.Localization;

using Microsoft.Extensions.Localization;

using Volo.Abp.Settings;

namespace Ediux.HomeSystem.Settings
{
    public class HomeSystemSettingDefinitionProvider : SettingDefinitionProvider
    {
        private readonly IStringLocalizer<HomeSystemResource> _localizer;

        public HomeSystemSettingDefinitionProvider(IStringLocalizer<HomeSystemResource> localizer)
        {
            _localizer = localizer;
        }

        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(HomeSystemSettings.MySetting1));

            context.Add(new SettingDefinition(HomeSystemSettings.SiteName, _localizer.GetString(HomeSystemResource.Common.SiteName), isVisibleToClients: true));
            //context.Add(new SettingDefinition(HomeSystemSettings.Root_SmartSettings_Theme_Role, "Administrator"));
        }
    }
}

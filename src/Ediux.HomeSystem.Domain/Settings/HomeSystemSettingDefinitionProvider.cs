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

            //context.Add(new SettingDefinition(HomeSystemSettings.WelcomeSlogan, defaultValue: defaultSloganHtml, isVisibleToClients: true));



            //DashBoardWidgetOptionDTOs defaultWidgets = new DashBoardWidgetOptionDTOs();
            //defaultWidgets.Widgets = new WidgetInformationDTO[] {
            //    new WidgetInformationDTO() { Name = "WelcomeWidget", DisplayName="Welcome Slogan Widget", Default=true, Order = 0 },
            //    new WidgetInformationDTO() { Name = "ABPHelpWidget", DisplayName="ABP Framework ReadMe Widget",Order = 1 } };
            //context.Add(new SettingDefinition(HomeSystemSettings.AvailableDashBoardWidgets, defaultValue: System.Text.Json.JsonSerializer.Serialize(defaultWidgets)));
            //context.Add(new SettingDefinition(HomeSystemSettings.UserSettings.DashBoard_Widgets, defaultValue: string.Empty, isVisibleToClients: true));
            //context.Add(new SettingDefinition(HomeSystemSettings.AvailableComponents, defaultValue: "DashboardWidgetGroupComponents,WebSettingsGroupComponents", isVisibleToClients: true));
            //context.Add(new SettingDefinition(HomeSystemSettings.UserSettings.Components, defaultValue: string.Empty, isVisibleToClients: true));
            //context.Add(new SettingDefinition(HomeSystemSettings.Root_SmartSettings_Theme_Role, "Administrator"));
        }
    }
}

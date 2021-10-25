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

            context.Add(new SettingDefinition(HomeSystemSettings.SiteName, _localizer.GetString(HomeSystemResource.Settings.WebSettings.WebSite), isVisibleToClients: true));
            string defaultSloganHtml = @"        <div class=""d-inline-block bg-success text-white p-1 h5 rounded mb-4 "" role=""alert"">
            <h5 class=""m-1""> <i class=""fas fa-rocket""></i> Congratulations, <strong>HomeSystem</strong> is successfully running!</h5>
        </div>
        <h1>Welcome to the Application</h1>

        <p class=""lead px-lg-5 mx-lg-5"">" + _localizer.GetString(HomeSystemResource.Common.LongWelcomeMessage) + @"</p>";
            context.Add(new SettingDefinition(HomeSystemSettings.WelcomeSlogan, defaultValue: defaultSloganHtml, isVisibleToClients: true));
            DashBoardWidgetOption defaultWidgets = new DashBoardWidgetOption();
            defaultWidgets.Widgets = new WidgetInformation[] { new WidgetInformation() { Name = "WelcomeWidget", Order = 0 }, new WidgetInformation() { Name = "ABPHelpWidget", Order = 1 } };
            context.Add(new SettingDefinition(HomeSystemSettings.AvailableDashBoardWidgets, defaultValue: System.Text.Json.JsonSerializer.Serialize(defaultWidgets)));
            context.Add(new SettingDefinition(HomeSystemSettings.UserSettings.DashBoard_Widgets, defaultValue: string.Empty, isVisibleToClients: true));
            //context.Add(new SettingDefinition(HomeSystemSettings.Root_SmartSettings_Theme_Role, "Administrator"));
        }
    }
}

﻿using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.DashBoard;

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
            string defaultSloganHtml = @"<div class=""p-5 text-center"">
    <div class=""d-inline-block bg-success text-white p-1 h5 rounded mb-4 "" role=""alert"">
		<h5 class=""m-1""> <i class=""fas fa-rocket""></i> Congratulations, <strong id=""websitename"">HomeSystem</strong> is successfully running!</h5>
	</div>
	<h1>Welcome to the Application</h1>
	<p class=""lead px-lg-5 mx-lg-5"" id=""welcome_area"">
	</p>
</div>";
            context.Add(new SettingDefinition(HomeSystemSettings.WelcomeSlogan, defaultValue: defaultSloganHtml, isVisibleToClients: true));
            DashBoardWidgetOptionDTOs defaultWidgets = new DashBoardWidgetOptionDTOs();
            defaultWidgets.Widgets = new WidgetInformationDTO[] {
                new WidgetInformationDTO() { Name = "WelcomeWidget", DisplayName="Welcome Slogan Widget", Default=true, Order = 0 },
                new WidgetInformationDTO() { Name = "ABPHelpWidget", DisplayName="ABP Framework ReadMe Widget",Order = 1 } };
            context.Add(new SettingDefinition(HomeSystemSettings.AvailableDashBoardWidgets, defaultValue: System.Text.Json.JsonSerializer.Serialize(defaultWidgets)));
            context.Add(new SettingDefinition(HomeSystemSettings.UserSettings.DashBoard_Widgets, defaultValue: string.Empty, isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.AvailableComponents, defaultValue: "DashboardWidgetGroupComponents,WebSettingsGroupComponents", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.UserSettings.Components, defaultValue: string.Empty, isVisibleToClients: true));
            //context.Add(new SettingDefinition(HomeSystemSettings.Root_SmartSettings_Theme_Role, "Administrator"));
        }
    }
}

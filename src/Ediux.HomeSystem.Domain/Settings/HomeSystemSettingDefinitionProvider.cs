using Ediux.HomeSystem.Localization;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp.Data;
using Volo.Abp.Settings;

namespace Ediux.HomeSystem.Settings
{
    public class HomeSystemSettingDefinitionProvider : SettingDefinitionProvider
    {
        private readonly IStringLocalizer<HomeSystemResource> _localizer;
        private readonly IOptionsSnapshot<AbpDbConnectionOptions> _options;

        public HomeSystemSettingDefinitionProvider(IStringLocalizer<HomeSystemResource> localizer,
            IOptionsSnapshot<AbpDbConnectionOptions> options)
        {
            _localizer = localizer;
            _options = options;
        }

        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(HomeSystemSettings.MySetting1));

            context.Add(new SettingDefinition(HomeSystemSettings.SiteName, _localizer.GetString(HomeSystemResource.Common.SiteName), isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.AppId, "1:971007962832:web:cd25c2c3f47fc7d0b678d5", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.ProjectId, "my-home-information-system", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.MeasurementId, "G-ZYEMRGY1NF", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.AuthDomain, "my-home-information-system.firebaseapp.com", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.ApiKey, "AIzaSyDSD7bp6UTIY0Qm4gAPG8o2WyoXMrVHJPE", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.ServiceKey, "AAAA4hSVrtA:APA91bFnZB7QheYVLd53jpVmm3d34ChYn8IcYObZYdzN3pWHAIngpm5Q7-rXLeR3ahjri2x3FwspLkx_EbapSm2GO_p6eGIMBGKHDQ0XbvCEX35CxF9_knlYckiEJUdmRq4jVG9rHfyy", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.StorageBucket, "my-home-information-system.appspot.com", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.MessagingSenderId, "971007962832", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.FCMVersion, "9.4.1", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.FCMSettings.VAPIdKey, "BOWFO614jRQJi1Hti7OUC_gpOvDnLHeF2hTbUoAkAbjgncx2hscixT6ZtMlrMWX9iwk6qGVwRRwF1jSqlBYqao0", isVisibleToClients: true));
            context.Add(new SettingDefinition(HomeSystemSettings.BatchSettings.Timer_Period, "600000", isVisibleToClients: true));
            context.Add(new SettingDefinition("ConnectionStrings_CmsKit", _options.Value.ConnectionStrings.Default , isVisibleToClients: true));
        }
    }
}

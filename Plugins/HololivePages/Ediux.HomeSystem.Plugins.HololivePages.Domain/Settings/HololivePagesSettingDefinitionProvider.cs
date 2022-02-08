using Microsoft.Extensions.Options;
using Volo.Abp.Data;
using Volo.Abp.Settings;

namespace Ediux.HomeSystem.Plugins.HololivePages.Settings
{
    public class HololivePagesSettingDefinitionProvider : SettingDefinitionProvider
    {
        private readonly IOptionsSnapshot<AbpDbConnectionOptions> _options;

        public HololivePagesSettingDefinitionProvider(IOptionsSnapshot<AbpDbConnectionOptions> options)
        {
            _options = options;
        }

        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from HololivePagesSettings class.
             */

            context.Add(new SettingDefinition(HololivePagesSettings.ConnectionStringSetting, _options.Value.ConnectionStrings.Default, isVisibleToClients: true));
        }
    }
}
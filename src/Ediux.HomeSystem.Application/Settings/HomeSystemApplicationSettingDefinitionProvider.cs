using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Options;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

using System.Linq;

using Volo.Abp.Settings;

namespace Ediux.HomeSystem.Settings
{
    public class HomeSystemApplicationSettingDefinitionProvider : SettingDefinitionProvider
    {
        private readonly IStringLocalizer<HomeSystemResource> _localizer;
        private readonly IOptions<DashboardWidgetOptions> options;

        public HomeSystemApplicationSettingDefinitionProvider(
            IStringLocalizer<HomeSystemResource> localizer,
            IOptions<DashboardWidgetOptions> options)
        {
            _localizer = localizer;
            this.options = options;
        }

        public override void Define(ISettingDefinitionContext context)
        {
            if (options.Value.Widgets.Any())
            {
                foreach (DashBoardWidgetsDTO widget in options.Value.Widgets.Values)
                {
                    if (!string.IsNullOrWhiteSpace(widget.GlobalSettingName))
                    {
                        context.Add(new SettingDefinition(widget.GlobalSettingName, defaultValue: widget.GlobalSettingDefaultValue, isVisibleToClients: true));                        
                    }
                }
            }
        }
    }
}

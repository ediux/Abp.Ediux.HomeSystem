
using Ediux.HomeSystem.AdditionalSystemFunctions4Users;

using Microsoft.Extensions.Localization;

using System;
using System.Collections.Generic;

namespace Ediux.HomeSystem.Options
{
    public class DashboardWidgetOption
    {
        public Dictionary<Type, DashBoardWidgetsDto> Widgets { get; }

        public IStringLocalizerFactory LocalizerProvider { get; set; }

        public DashboardWidgetOption()
        {
            Widgets = new Dictionary<Type, DashBoardWidgetsDto>();
        }

        //public void Add<TWidget>(string displayName, bool HasOption = true, bool AllowUserSetting = false, bool reuse = false, bool isDefault = true, int order = -1)
        //{
        //    Type widgetType = typeof(TWidget);

        //    if (Widgets.ContainsKey(widgetType) == false)
        //    {
        //        Widgets.Add(widgetType, new DashBoardWidgetsDTO()
        //        {
        //            DisplayName = _localizer[displayName].ResourceNotFound ? displayName : _localizer[displayName].Value,
        //            Name = widgetType.Name,
        //            AllowMulti = reuse,
        //            AllowUserSetting = AllowUserSetting,
        //            HasOption = HasOption,
        //            IsDefault = isDefault,
        //            Order = (order == -1) ? Widgets.Max(m => m.Value.Order) + 1 : order
        //        });
        //    }
        //}
    }
}

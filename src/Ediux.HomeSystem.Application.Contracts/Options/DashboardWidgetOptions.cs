using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.DashBoard;

using Microsoft.Extensions.Localization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ediux.HomeSystem.Options
{
    public class DashboardWidgetOptions
    {
        public Dictionary<Type, DashBoardWidgetsDTO> Widgets { get; }

        public IStringLocalizerFactory LocalizerProvider { get; set; }

        public DashboardWidgetOptions()
        {
            Widgets = new Dictionary<Type, DashBoardWidgetsDTO>();
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

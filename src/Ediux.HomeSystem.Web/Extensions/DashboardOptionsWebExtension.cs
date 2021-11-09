using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Options;

using System;
using System.Linq;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace Ediux.HomeSystem.Web.Extensions
{
    public static class DashboardOptionsWebExtension
    {
        public static DashBoardWidgetsDTO Add<TWidget>(this DashboardWidgetOptions options, string defaultDisplayName, bool CanReuse = false, bool DefaultEnabled = false) where TWidget : AbpViewComponent
        {
            Type widgetType = typeof(TWidget);

            DashBoardWidgetsDTO widgetInfo = new DashBoardWidgetsDTO()
            {
                Name = widgetType.Name.Replace("ViewComponent", ""),
                DisplayName = defaultDisplayName,
                AllowMulti = CanReuse,
                AllowUserSetting = false,
                HasOption = false,
                IsDefault = DefaultEnabled,
                Order = options.Widgets.Count == 0 ? 0 : (options.Widgets.Max(m => m.Value.Order) + 1),
                GlobalSettingName = null,
                GlobalSettingDefaultValue = null
            };

            WidgetAttribute widgetAttribute = (WidgetAttribute)widgetType.GetCustomAttributes(typeof(WidgetAttribute), true).SingleOrDefault();

            if (widgetAttribute != null)
            {
                if (widgetAttribute.DisplayNameResource != null)
                {
                    var localStr = options.LocalizerProvider.Create(widgetAttribute.DisplayNameResource);

                    if (!localStr[widgetAttribute.DisplayName].ResourceNotFound)
                    {
                        widgetInfo.DisplayName = localStr[widgetAttribute.DisplayName].Value;
                    }
                    else
                    {
                        widgetInfo.DisplayName = defaultDisplayName;
                    }
                }
                else
                {
                    widgetInfo.DisplayName = widgetAttribute.DisplayName ?? defaultDisplayName;
                }
            }

            if (options.Widgets.ContainsKey(widgetType) == false)
            {
                options.Widgets.Add(widgetType, widgetInfo);
            }

            return widgetInfo;
        }

        public static DashBoardWidgetsDTO ChangeDefaultName(this DashBoardWidgetsDTO widget, string name)
        {
            widget.Name = name;
            return widget;
        }
    }
}

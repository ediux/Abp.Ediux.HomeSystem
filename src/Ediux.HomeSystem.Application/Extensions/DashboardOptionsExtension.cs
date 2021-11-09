using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Options;

using System;

namespace Ediux.HomeSystem.Extensions
{
    public static class DashboardOptionsExtension
    {

        public static DashBoardWidgetsDTO SetPermissionName(this DashBoardWidgetsDTO widgetInfo, string permissionName)
        {
            widgetInfo.PermissionName = permissionName;
            return widgetInfo;
        }

        public static DashBoardWidgetsDTO SetGlobalSettingName(this DashBoardWidgetsDTO widgetInfo, string globalSettingName, string defaultvalue = "")
        {
            widgetInfo.GlobalSettingName = globalSettingName;
            widgetInfo.GlobalSettingDefaultValue = defaultvalue;
            return widgetInfo;
        }

        public static DashBoardWidgetsDTO UseSettingManagementUI(this DashBoardWidgetsDTO widgetInfo)
        {
            widgetInfo.HasOption = true;
            return widgetInfo;
        }

        public static DashBoardWidgetsDTO EnableUserSetting(this DashBoardWidgetsDTO widgetInfo)
        {
            if (widgetInfo.HasOption == false)
                widgetInfo.HasOption = true;

            widgetInfo.AllowUserSetting = true;

            return widgetInfo;
        }
    }
}

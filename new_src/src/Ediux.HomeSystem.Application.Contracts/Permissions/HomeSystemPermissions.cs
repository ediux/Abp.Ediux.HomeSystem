using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.Permissions
{
    public static class HomeSystemPermissions
    {
        public const string GroupName = "HomeSystem";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
        
        public class Files
        {
            public const string Prefix = GroupName + "." + nameof(Files);
            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
            public const string Export = Prefix + SubAction.Export;
        }
        public class MIMETypeManager
        {
            public const string Prefix = GroupName + "." + nameof(MIMETypeManager);
            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
        }
        public class PluginsManager
        {
            public const string Prefix = GroupName + ".PluginsManager";
            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
        }

        public class ProductKeysBook
        {
            public const string Prefix = GroupName + ".ProductKeysBook";

            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
        }

        public class PasswordBook
        {
            public const string Prefix = GroupName + ".PasswordBook";

            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
        }

        public class PersonalCalendar
        {
            public const string Prefix = GroupName + "." + nameof(PersonalCalendar);
            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
        }

        public class TabViewerWidget
        {
            public const string Prefix = GroupName + "." + nameof(TabViewerWidget);
            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
        }

        public class Docs
        {
            public const string Prefix = GroupName + ".Documents";
            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
        }


        public class Home
        {
            public const string Prefix = GroupName + ".HomePage";

            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
        }
        public class Photos
        {
            public const string Prefix = GroupName + "." + nameof(Photos);
            public const string CreateNew = Prefix + SubAction.CreateNew;
            public const string Delete = Prefix + SubAction.Delete;
            public const string Execute = Prefix + SubAction.Execute;
            public const string Lists = Prefix + SubAction.Lists;
            public const string Modify = Prefix + SubAction.Modify;
            public const string Special = Prefix + SubAction.Special;
            public const string Widget = Prefix + SubAction.Widget;
            public const string Options = Prefix + SubAction.Options;
            public const string Export = Prefix + SubAction.Export;
        }
        public class Settings
        {
            public const string Prefix = SettingManagementPermissions.GroupName + ".SystemSetting";

            public const string Execute = Prefix + SubAction.Execute;

            public const string Special = Prefix + SubAction.Special;
        }

        public class SubAction
        {
            public const string Execute = ".Executed";
            public const string CreateNew = ".CreateNew";
            public const string Lists = ".List";
            public const string Modify = ".Modify";
            public const string Delete = ".Delete";
            public const string Special = ".Special";
            public const string Widget = ".Widget";
            public const string Options = ".Options";
            public const string Export = ".Export";
        }
    }
}
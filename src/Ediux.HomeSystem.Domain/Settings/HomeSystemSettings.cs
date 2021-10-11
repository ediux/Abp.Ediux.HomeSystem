using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Settings
{
    public static class HomeSystemSettings
    {
        public const string Prefix = "HomeSystem";

        //Add your own setting names here. Example:
        //public const string MySetting1 = Prefix + ".MySetting1";

        public const string SiteName = Prefix + "." + nameof(SiteName);

        //internal const string Section_Root = "SmartSettings";

        //internal const string Section_Theme = "Theme";

        //internal const string Section_Features = "Features";

        //internal const string Root_SmartSettings_Version = Prefix + "." + Section_Root + ".Version";

        //internal const string Root_SmartSettings_App = Prefix + "." + Section_Root + ".App";

        //internal const string Root_SmartSettings_AppName = Prefix + "." + Section_Root + ".AppName";

        //internal const string Root_SmartSettings_AppFlavor = Prefix + "." + Section_Root + ".AppFlavor";

        //internal const string Root_SmartSettings_CORSAllowHosts = Prefix + "." + Section_Root + ".CORSAllowHosts";

        //internal const string Root_SmartSettings_AppFlavorSubscript = Prefix + "." + Section_Root + ".AppFlavorSubscript";

        //internal const string Root_SmartSettings_Theme_ThemeVersion = Prefix + "." + Section_Root + "." + Section_Theme + ".ThemeVersion";

        //internal const string Root_SmartSettings_Theme_IconPrefix = Prefix + "." + Section_Root + "." + Section_Theme + ".IconPrefix";

        //internal const string Root_SmartSettings_Theme_Logo = Prefix + "." + Section_Root + "." + Section_Theme + ".Logo";

        //internal const string Root_SmartSettings_Theme_User = Prefix + "." + Section_Root + "." + Section_Theme + ".User";

        //internal const string Root_SmartSettings_Theme_Role = Prefix + "." + Section_Root + "." + Section_Theme + ".Role";

        //internal const string Root_SmartSettings_Theme_Email = Prefix + "." + Section_Root + "." + Section_Theme + ".Email";

        //internal const string Root_SmartSettings_Theme_Twitter = Prefix + "." + Section_Root + "." + Section_Theme + ".Twitter";

        //internal const string Root_SmartSettings_Theme_Avatar = Prefix + "." + Section_Root + "." + Section_Theme + ".Avatar";

        //internal const string Root_SmartSettings_Features_AppSidebar = Prefix + "." + Section_Root + "." + Section_Features + ".AppSidebar";

        //internal const string Root_SmartSettings_Features_AppHeader = Prefix + "." + Section_Root + "." + Section_Features + ".AppHeader";

        //internal const string Root_SmartSettings_Features_AppLayoutShortcut = Prefix + "." + Section_Root + "." + Section_Features + ".AppLayoutShortcut";

        //internal const string Root_SmartSettings_Features_AppFooter = Prefix + "." + Section_Root + "." + Section_Features + ".AppFooter";

        //internal const string Root_SmartSettings_Features_ShortcutMenu = Prefix + "." + Section_Root + "." + Section_Features + ".ShortcutMenu";

        //internal const string Root_SmartSettings_Features_GoogleAnalytics = Prefix + "." + Section_Root + "." + Section_Features + ".GoogleAnalytics";

        //internal const string Root_SmartSettings_Features_ChatInterface = Prefix + "." + Section_Root + "." + Section_Features + ".ChatInterface";

        //internal const string Root_SmartSettings_Features_LayoutSettings = Prefix + "." + Section_Root + "." + Section_Features + ".LayoutSettings";
    }

    public class SmartError : ITransientDependency
    {
        public string[][] Errors { get; set; } = System.Array.Empty<string[]>();

        public static SmartError Failed(params string[] errors) => new SmartError { Errors = new[] { errors } };
    }
}
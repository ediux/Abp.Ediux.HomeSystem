namespace Ediux.HomeSystem.Settings
{
    public static class HomeSystemSettings
    {
        public const string Prefix = "HomeSystem";

        //Add your own setting names here. Example:
        //public const string MySetting1 = Prefix + ".MySetting1";

        public const string SiteName = Prefix + "." + nameof(SiteName);

        /// <summary>
        /// 網站歡迎標語設定
        /// </summary>
        public const string WelcomeSlogan = Prefix + "." + nameof(WelcomeSlogan);

        /// <summary>
        /// 目前預設可用的Widget
        /// </summary>
        public const string AvailableDashBoardWidgets = Prefix + "." + nameof(AvailableDashBoardWidgets);

        /// <summary>
        /// 目前已註冊的可用元件清單(JSON)
        /// </summary>
        public const string AvailableComponents = Prefix + "." + nameof(AvailableComponents);

        public class UserSettings
        {
            public const string Prefix = HomeSystemSettings.Prefix + ".Users";

            /// <summary>
            /// 使用者Widget設定
            /// </summary>
            public const string DashBoard_Widgets = Prefix + "." + nameof(DashBoard_Widgets);

            /// <summary>
            /// 使用者可用的元件清單
            /// </summary>
            public const string Components = Prefix + "." + nameof(Components);
        }

        
    }
}
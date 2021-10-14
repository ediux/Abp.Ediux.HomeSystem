namespace Ediux.HomeSystem.Web.Menus
{
    public class HomeSystemMenus
    {
        public const string Prefix = "HomeSystem";
        public const string Home = Prefix + ".Home";
        public const string Docs = Prefix + ".Docs";
        //Add your menu items here...
        public const string PluginsManager = Prefix + ".PluginsManager";
        public const string ProductKeysBook = Prefix + ".ProductKeysBook";
        public const string PasswordBook = Prefix + ".PasswordBook";
        public const string MIMETypeManager = Prefix + ".MIMETypeManager";
        public const string PersonalCalendar = Prefix + ".PersonalCalendar";
        public const string Features = Prefix + "."+nameof(Features);
    }
}
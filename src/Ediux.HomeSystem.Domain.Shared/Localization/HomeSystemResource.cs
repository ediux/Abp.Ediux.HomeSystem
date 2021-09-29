using Volo.Abp.Localization;

namespace Ediux.HomeSystem.Localization
{
    [LocalizationResourceName("HomeSystem")]
    public class HomeSystemResource
    {
        public class Menu
        {

            public const string ProductKeysBook = "Menu:ProductKeysBook";
            public const string PluginsManager = "Menu:PluginsManager";
            public const string PasswordBook = "Menu:PasswordBook";
            public const string Home = "Menu:Home";
            public const string Documents = "Menu:Docs";
        }
        
        public class Common
        {
            public const string PluginsManager = "PluginsManager";
            public const string SiteName = "SiteName";
            public const string Welcome = "Welcome";
            public const string LongWelcomeMessage = "LongWelcomeMessage";
        }

        public class Permissions
        {
            public const string ProductKeysBook = "HomeSystemPermissions.ProductKeysBook";
            public const string PasswordBook = "HomeSystemPermissions.PasswordBook";
        }

        public class Buttons
        {
            public const string Delete = "Button:Delete";
            public const string Add = "Button:Add";
            public const string Save = "Button:Save";
            public const string Insert = "Button:Insert";
            public const string Update = "Button:Update";
            public const string Edit = "Button:Edit";            
        }

        public class Features
        {
            public class ProductKeysBook
            {
                public class DTFX
                {
                    public class Columns
                    {
                        public const string ProductName = "ProductKeysBook.Columns.ProductName";
                        public const string ProductKey = "ProductKeysBook.Columns.ProductKey";
                        public const string Flag_Shared = "ProductKeysBook.Columns.Shared";
                    }
                }
            }

            public class PasswordBook
            {
                public class DTFX
                {
                    public class Columns
                    {
                        public const string SiteName = "Features:PasswordBook.Columns.SiteName";

                        public const string LoginAccount = "Features:PasswordBook.Columns.LoginAccount";

                        public const string Password = "Features:PasswordBook.Columns.Password";

                        public const string SiteURL = "Features:PasswordBook.Columns.SiteURL";

                        public const string IsHistory = "Features:PasswordBook.Columns.IsHistory";
                    }
                }
            }
        }
    }
}
using Volo.Abp.Localization;

namespace Ediux.HomeSystem.Localization
{
    [LocalizationResourceName("HomeSystem")]
    public class HomeSystemResource
    {
        public class Menu
        {
            public const string ProductKeysBook = "Menu:ProductKeysBook";
            public const string Documents = "Menu:Docs";
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
    }
}
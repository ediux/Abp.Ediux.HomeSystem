using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.AspNetCore.Mvc.UI.Theme.SmartAdmin
{
    [ThemeName(Name)]
    public class SmartAdminTheme : ITheme, ITransientDependency
    {
        public const string Name = "SmartAdmin";

        public virtual string GetLayout(string name, bool fallbackToDefault = true)
        {
            switch (name)
            {
                case StandardLayouts.Application:
                    return "~/Themes/SmartAdmin/Layouts/_Layout.cshtml";
                case StandardLayouts.Account:
                    return "~/Themes/SmartAdmin/Layouts/Account.cshtml";
                case StandardLayouts.Empty:
                    return "~/Themes/SmartAdmin/Layouts/Empty.cshtml";
                default:
                    return fallbackToDefault ? "~/Themes/SmartAdmin/Layouts/Application.cshtml" : null;
            }
        }
    }
}

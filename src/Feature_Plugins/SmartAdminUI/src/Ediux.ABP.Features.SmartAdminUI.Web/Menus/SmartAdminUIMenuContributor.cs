using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Ediux.ABP.Features.SmartAdminUI.Web.Menus
{
    public class SmartAdminUIMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(SmartAdminUIMenus.Prefix, displayName: "SmartAdminUI", "~/SmartAdminUI", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}
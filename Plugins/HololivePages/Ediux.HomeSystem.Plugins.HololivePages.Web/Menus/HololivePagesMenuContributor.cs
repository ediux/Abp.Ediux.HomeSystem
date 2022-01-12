using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Ediux.HomeSystem.Plugins.HololivePages.Web.Menus
{
    public class HololivePagesMenuContributor : IMenuContributor
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
            context.Menu.AddItem(new ApplicationMenuItem(HololivePagesMenus.Prefix, displayName: "HololivePages", "~/HololivePages", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}
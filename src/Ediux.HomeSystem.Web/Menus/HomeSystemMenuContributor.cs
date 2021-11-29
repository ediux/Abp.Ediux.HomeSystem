using System.Threading.Tasks;

using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.Web.Menus
{
    public class HomeSystemMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }

            if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<HomeSystemResource>();

            if (await context.IsGrantedAsync(HomeSystemPermissions.Home.Execute))
            {
                context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    HomeSystemMenus.Home,
                    l[HomeSystemResource.Menu.Home],
                    "~/",
                    icon: "fas fa-tachometer",
                    order: 0
                ));
            }

            var user = (ICurrentUser)context.ServiceProvider.GetService(typeof(ICurrentUser));

            if (user != null && user.IsAuthenticated)
            {
                var mainMenu = new ApplicationMenuItem(HomeSystemMenus.Features,
                   l[HomeSystemResource.Menu.Features],
                   "#");           
                
                context.Menu.Items.Insert(1, mainMenu);
            }
           
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

            if (await context.IsGrantedAsync(HomeSystemPermissions.PluginsManager.Prefix))
            {
                administration.AddItem(new ApplicationMenuItem(
                    HomeSystemMenus.PluginsManager,
                    l[HomeSystemResource.Menu.PluginsManager].Value,
                    "~/PluginsManager",
                    icon: "fas fa-puzzle-piece",
                    order: 0
                    ));
            }

            if (await context.IsGrantedAsync(HomeSystemPermissions.MIMETypeManager.Options))
            {
                administration.AddItem(new ApplicationMenuItem(HomeSystemMenus.MIMETypeManager,
                    l[HomeSystemResource.Menu.MIMETypesManager],
                    "~/MIMETypeManager"));
            }

        }

        private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<HomeSystemResource>();

            if (await context.IsGrantedAsync(HomeSystemPermissions.ProductKeysBook.Execute))
            {
                context.Menu.Items.Insert(1, new ApplicationMenuItem(
                    HomeSystemMenus.ProductKeysBook,
                    l[HomeSystemResource.Menu.ProductKeysBook],
                    "~/ProductKeysBook/Index",
                    icon: "fas fa-key",
                    order: 0));
            }
            if (await context.IsGrantedAsync(HomeSystemPermissions.PasswordBook.Execute))
            {                
                context.Menu.Items.Insert(2, new ApplicationMenuItem(
                    HomeSystemMenus.PasswordBook,
                    l[HomeSystemResource.Menu.PasswordBook],
                    "~/PasswordBook",
                    icon: "fas fa-id-card",
                    order: 0));
            }
            if (await context.IsGrantedAsync(HomeSystemPermissions.PersonalCalendar.Execute))
            {
                context.Menu.Items.Insert(3, new ApplicationMenuItem(
                    HomeSystemMenus.PersonalCalendar,
                    l[HomeSystemResource.Menu.PersonalCalendar],
                    "~/PersonalCalendar",
                    icon: "fas fa-calendar",
                    order: 0));
            }
        }
    }
}

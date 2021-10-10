using System.Threading.Tasks;

using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.MultiTenancy;
using Ediux.HomeSystem.Permissions;

using Volo.Abp.Authorization.Permissions;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

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

            if(context.Menu.Name == StandardMenus.User)
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
                    icon: "fas fa-home",
                    order: 0
                )
            );
            }
            


            if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

            if (await context.IsGrantedAsync(FeatureManagementPermissions.ManageHostFeatures))
            {
                administration.AddItem(new ApplicationMenuItem(
                    HomeSystemMenus.PluginsManager,
                    l[HomeSystemResource.Menu.PluginsManager],
                    "~/PluginsManager",
                    icon: "fas fa-puzzle-piece",
                    order: 0
                    ));
            }
           

            if (await context.IsGrantedAsync(HomeSystemPermissions.DocumentsList))
            {
                context.Menu.Items.Add(new ApplicationMenuItem(HomeSystemMenus.Docs, l[HomeSystemResource.Menu.Docs], "/Documents"));
            }
        }
    
        private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<HomeSystemResource>();

            if (await context.IsGrantedAsync(HomeSystemPermissions.ProductKeysBook))
            {
                context.Menu.Items.Insert(1, new ApplicationMenuItem(
                    HomeSystemMenus.ProductKeysBook,
                    l[HomeSystemResource.Menu.ProductKeysBook],
                    "~/ProductKeysBook",
                    icon: "fas fa-key",
                    order: 0));
            }
            if (await context.IsGrantedAsync(HomeSystemPermissions.PasswordBook))
            {
                context.Menu.Items.Insert(2, new ApplicationMenuItem(
                    HomeSystemMenus.PasswordBook,
                    l[HomeSystemResource.Menu.PasswordBook],
                    "~/PassworkBook",
                    icon: "fas fa-key",
                    order: 0));
            }
        }
    }
}

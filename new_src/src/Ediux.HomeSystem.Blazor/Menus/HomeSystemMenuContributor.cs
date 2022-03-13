using Blazorise;

using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;

using System;
using System.Threading.Tasks;

using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace Ediux.HomeSystem.Blazor.Menus
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

            context.Menu.AddItem(                
                new ApplicationMenuItem(
                    HomeSystemMenus.Home,
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home",
                    order: 0
                )
            );

            if (Environment.GetEnvironmentVariable("AbpMultiTenancy") == "Enabled")
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

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
                context.Menu.AddItem(new ApplicationMenuItem(
                    HomeSystemMenus.ProductKeysBook,
                    l[HomeSystemResource.Menu.ProductKeysBook],
                    url: "~/ProductKeys",
                    order: 0)
                { Icon = "fas fa-key" });
            }
            if (await context.IsGrantedAsync(HomeSystemPermissions.PasswordBook.Execute))
            {
                context.Menu.AddItem(new ApplicationMenuItem(
                    HomeSystemMenus.PasswordBook,
                    l[HomeSystemResource.Menu.PasswordBook],
                    "~/PasswordStore",
                    icon: "fas fa-id-card",
                    order: 0));
            }
            if (await context.IsGrantedAsync(HomeSystemPermissions.PersonalCalendar.Execute))
            {
                context.Menu.AddItem(new ApplicationMenuItem(
                    HomeSystemMenus.PersonalCalendar,
                    l[HomeSystemResource.Menu.PersonalCalendar],
                    "~/PersonalCalendar",
                    icon: "fas fa-calendar",
                    order: 0));
            }
            if (await context.IsGrantedAsync(HomeSystemPermissions.Files.Execute))
            {
                context.Menu.AddItem(new ApplicationMenuItem(
                    HomeSystemMenus.Files,
                    l[HomeSystemResource.Menu.Files],
                    "~/Files",
                    icon: "fas fa-file",
                    order: 0));
            }
            if (await context.IsGrantedAsync(HomeSystemPermissions.Photos.Execute))
            {
                context.Menu.AddItem(new ApplicationMenuItem(
                    HomeSystemMenus.Photos,
                    l[HomeSystemResource.Menu.Photos],
                    "~/Photos",
                    icon: "fas fa-photo-video",
                    order: 0));
            }
        }
    }
}

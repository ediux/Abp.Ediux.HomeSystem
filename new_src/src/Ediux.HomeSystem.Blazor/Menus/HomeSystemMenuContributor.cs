using System;
using System.Threading.Tasks;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.MultiTenancy;
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
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<HomeSystemResource>();

            context.Menu.Items.Insert(
                0,
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

            return Task.CompletedTask;
        }
    }
}

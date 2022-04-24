using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ediux.HomeSystem.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using Ediux.HomeSystem.Permissions;

namespace Ediux.HomeSystem.Blazor.Menus;

public class HomeSystemMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public HomeSystemMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<HomeSystemResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                HomeSystemMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );

        if (await context.IsGrantedAsync(HomeSystemPermissions.Blogs.Execute))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                HomeSystemMenus.Blogs,
                l[HomeSystemResource.Menu.Blogs],
                "/Blogs",
                icon: "fas fa-archive",
                order: 0));
        }
      
    }

    private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        var l = context.GetLocalizer<HomeSystemResource>();

        if (await context.IsGrantedAsync(HomeSystemPermissions.ProductKeysBook.Execute))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                HomeSystemMenus.ProductKeysBook,
                l[HomeSystemResource.Menu.ProductKeysBook],
                url: "/ProductKeys",
                order: 0)
            { Icon = "fas fa-key" });
        }
        if (await context.IsGrantedAsync(HomeSystemPermissions.PasswordBook.Execute))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                HomeSystemMenus.PasswordBook,
                l[HomeSystemResource.Menu.PasswordBook],
                "/PasswordStore",
                icon: "fas fa-id-card",
                order: 0));
        }
        if (await context.IsGrantedAsync(HomeSystemPermissions.PersonalCalendar.Execute))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                HomeSystemMenus.PersonalCalendar,
                l[HomeSystemResource.Menu.PersonalCalendar],
                "/PersonalCalendar",
                icon: "fas fa-calendar",
                order: 0));
        }
        if (await context.IsGrantedAsync(HomeSystemPermissions.Files.Execute))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                HomeSystemMenus.Files,
                l[HomeSystemResource.Menu.Files],
                "/Files",
                icon: "fas fa-file",
                order: 0));
        }
        if (await context.IsGrantedAsync(HomeSystemPermissions.Photos.Execute))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                HomeSystemMenus.Photos,
                l[HomeSystemResource.Menu.Photos],
                "/Photos",
                icon: "fas fa-photo-video",
                order: 0));
        }

       
    }   
}

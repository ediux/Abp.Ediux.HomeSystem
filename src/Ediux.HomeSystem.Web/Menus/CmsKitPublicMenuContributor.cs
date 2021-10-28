using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Volo.Abp.GlobalFeatures;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Localization;
using Volo.CmsKit.Menus;
using Volo.CmsKit.Public.Menus;

namespace Ediux.HomeSystem.Web.Menus
{
    public class CmsKitPublicMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {

                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<CmsKitResource>();

            if (GlobalFeatureManager.Instance.IsEnabled<MenuFeature>())
            {
                var user = (ICurrentUser)context.ServiceProvider.GetService(typeof(ICurrentUser));

                if (user.IsAuthenticated)
                {
                    var cmsMenu = context.Menu.FindMenuItem(HomeSystemMenus.Features);

                    var menuAppService = context.ServiceProvider.GetRequiredService<IMenuItemPublicAppService>();

                    var menuItems = await menuAppService.GetListAsync();

                    if (!menuItems.IsNullOrEmpty())
                    {
                        foreach (var menuItemDto in menuItems.Where(x => x.ParentId == null && x.IsActive))
                        {
                            AddChildItems(menuItemDto, menuItems, cmsMenu);
                        }
                    }

                    IBlogAdminAppService blogAdminAppService = context.ServiceProvider.GetRequiredService<IBlogAdminAppService>();

                    var blogs = await blogAdminAppService.GetListAsync(new BlogGetListInput() { });

                    if (blogs.Items.Any())
                    {
                        var blogrootmenu = new ApplicationMenuItem(
                            l["Blogs"].Value,
                            l["Blogs"].Value,
                            icon: "fa fa-blog");

                        foreach (var blog in blogs.Items)
                        {
                            blogrootmenu.AddItem(new ApplicationMenuItem(
                                "Blog:" + blog.Slug,
                                blog.Name,
                                url: "/blogs/" + blog.Slug));
                        }

                        cmsMenu.Items.Insert(0, blogrootmenu);
                    }
                }

            }
        }

        private void AddChildItems(MenuItemDto menuItem, List<MenuItemDto> source, IHasMenuItems parent = null)
        {
            var applicationMenuItem = CreateApplicationMenuItem(menuItem);

            foreach (var item in source.Where(x => x.ParentId == menuItem.Id && x.IsActive))
            {
                AddChildItems(item, source, applicationMenuItem);
            }

            parent?.Items.Add(applicationMenuItem);
        }

        private ApplicationMenuItem CreateApplicationMenuItem(MenuItemDto menuItem)
        {
            return new ApplicationMenuItem(
                menuItem.DisplayName,
                menuItem.DisplayName,
                menuItem.Url,
                menuItem.Icon,
                menuItem.Order,
                customData: null,
                menuItem.Target,
                menuItem.ElementId,
                menuItem.CssClass
            );
        }
    }
}
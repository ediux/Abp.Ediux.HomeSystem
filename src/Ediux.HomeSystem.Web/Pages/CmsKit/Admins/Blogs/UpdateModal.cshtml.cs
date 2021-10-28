using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Validation;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Admin.Menus;
using Volo.CmsKit.Blogs;

namespace Ediux.HomeSystem.Web.Pages.CmsKit.Admins.Blogs
{
    public class UpdateModalModel : CmsKitAdminPageModel
    {
        protected IBlogAdminAppService BlogAdminAppService { get; }
        protected IMenuItemAdminAppService MenuAdminAppService { get; }
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateBlogViewModel ViewModel { get; set; }

        public UpdateModalModel(IBlogAdminAppService blogAdminAppService,
            IMenuItemAdminAppService menuAdminAppService)
        {
            BlogAdminAppService = blogAdminAppService;
            MenuAdminAppService = menuAdminAppService;
        }

        public async Task OnGetAsync()
        {
            var blog = await BlogAdminAppService.GetAsync(Id);

            ViewModel = ObjectMapper.Map<BlogDto, UpdateBlogViewModel>(blog);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<UpdateBlogViewModel, UpdateBlogDto>(ViewModel);

            await BlogAdminAppService.UpdateAsync(Id, dto);
            //var blogmenuroot = (await MenuAdminAppService.GetListAsync()).Items
            //    .Where(w => w.DisplayName == L["Blogs"]).SingleOrDefault();

            //if (blogmenuroot == null)
            //{
            //    blogmenuroot = await MenuAdminAppService.CreateAsync(new MenuItemCreateInput()
            //    {
            //        DisplayName = L["Blogs"],
            //        Url = "#",
            //        Icon = "fa fa-blog",
            //    });
            //}

            //var currentblogmenu = (await MenuAdminAppService.GetListAsync())
            //   .Items.Where(w => w.Url == "/blogs/" + ViewModel.Slug)
            //   .SingleOrDefault();

            //if (currentblogmenu == null)
            //{
            //    currentblogmenu = await MenuAdminAppService.CreateAsync(new MenuItemCreateInput()
            //    {
            //        ParentId = blogmenuroot.Id,
            //        DisplayName = ViewModel.Name,
            //        Url = "/blogs/" + ViewModel.Slug
            //    });
            //}
            return NoContent();
        }

        [AutoMap(typeof(BlogDto))]
        [AutoMap(typeof(UpdateBlogDto), ReverseMap = true)]
        public class UpdateBlogViewModel
        {
            [Required]
            [DynamicMaxLength(typeof(BlogConsts), nameof(BlogConsts.MaxNameLength))]
            public string Name { get; set; }

            [DynamicMaxLength(typeof(BlogConsts), nameof(BlogConsts.MaxSlugLength))]
            [Required]
            public string Slug { get; set; }
        }
    }
}

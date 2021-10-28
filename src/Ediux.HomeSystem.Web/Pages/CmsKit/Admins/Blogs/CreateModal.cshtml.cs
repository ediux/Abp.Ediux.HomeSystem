using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Validation;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Admin.Menus;
using Volo.CmsKit.Blogs;

namespace Ediux.HomeSystem.Web.Pages.CmsKit.Admins.Blogs
{
    public class CreateModalModel : CmsKitAdminPageModel
    {
        protected IBlogAdminAppService BlogAdminAppService { get; }
        protected IMenuItemAdminAppService MenuAdminAppService { get; }
        [BindProperty]
        public CreateBlogViewModel ViewModel { get; set; }

        public CreateModalModel(IBlogAdminAppService blogAdminAppService,
            IMenuItemAdminAppService menuAdminAppService)
        {
            BlogAdminAppService = blogAdminAppService;
            MenuAdminAppService = menuAdminAppService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateBlogViewModel, CreateBlogDto>(ViewModel);

            await BlogAdminAppService.CreateAsync(dto);
            //var blogmenuroot = (await MenuAdminAppService.GetListAsync())
            //    .Items.Where(w => w.DisplayName == L["Blogs"]).SingleOrDefault();

            //if (blogmenuroot == null)
            //{
            //    blogmenuroot = await MenuAdminAppService.CreateAsync(new MenuItemCreateInput()
            //    {
            //        DisplayName = L["Blogs"],
            //        Url = "#",
            //        Icon = "fa fa-blog",
            //    });
            //}

            //await MenuAdminAppService.CreateAsync(new MenuItemCreateInput()
            //{
            //    ParentId = blogmenuroot.Id,
            //    DisplayName = ViewModel.Name,
            //    Url = "/blogs/" + ViewModel.Slug
            //});

            return NoContent();
        }

        [AutoMap(typeof(CreateBlogDto), ReverseMap = true)]
        public class CreateBlogViewModel
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

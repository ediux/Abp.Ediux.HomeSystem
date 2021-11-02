using AutoMapper;

using Ediux.HomeSystem.Miscellaneous;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Validation;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Blogs;

namespace Ediux.HomeSystem.Web.Pages.CmsKit.Admins.BlogPosts
{
    public class CreateModel : CmsKitAdminPageModel
    {
        protected IBlogPostAdminAppService BlogPostAdminAppService { get; }
        protected IMiscellaneousAppService MiscellaneousAppService { get; }

        [BindProperty]
        public CreateBlogPostViewModel ViewModel { get; set; }

        public CreateModel(
            IBlogPostAdminAppService blogPostAdminAppService,
            IMiscellaneousAppService miscellaneousAppService)
        {
            BlogPostAdminAppService = blogPostAdminAppService;
            MiscellaneousAppService = miscellaneousAppService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateBlogPostViewModel, CreateBlogPostDto>(ViewModel);

            var created = await BlogPostAdminAppService.CreateAsync(dto);
            
            await MiscellaneousAppService.RemoveAutoSaveDataAsync(new HomeSystem.Models.Views.AutoSaveModel()
            {
                entityType = "page",
                id = CurrentUser.Id.ToString()
            });

            return new OkObjectResult(created);
        }

        [AutoMap(typeof(CreateBlogPostDto), ReverseMap = true)]
        public class CreateBlogPostViewModel
        {
            [Required]
            [DynamicFormIgnore]
            public Guid BlogId { get; set; }

            [Required]
            [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxTitleLength))]
            [DynamicFormIgnore]
            public string Title { get; set; }

            [Required]
            [DynamicStringLength(
                typeof(BlogPostConsts),
                nameof(BlogPostConsts.MaxSlugLength),
                nameof(BlogPostConsts.MinSlugLength))]
            [DynamicFormIgnore]
            public string Slug { get; set; }

            [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxShortDescriptionLength))]
            public string ShortDescription { get; set; }

            [TextArea(Rows = 10)]
            [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxContentLength))]
            public string Content { get; set; }

            [HiddenInput]
            public Guid? CoverImageMediaId { get; set; }
        }
    }
}

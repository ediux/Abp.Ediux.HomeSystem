using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Validation;
using Volo.CmsKit.Admin.Pages;
using Volo.CmsKit.Pages;

namespace Ediux.HomeSystem.Web.Pages.CmsKit.Admins.Pages
{
    public class CreateModel : CmsKitAdminPageModel
    {
        protected readonly IPageAdminAppService pageAdminAppService;

        [BindProperty]
        public CreatePageViewModel ViewModel { get; set; }

        public CreateModel(IPageAdminAppService pageAdminAppService)
        {
            this.pageAdminAppService = pageAdminAppService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var createInput = ObjectMapper.Map<CreatePageViewModel, CreatePageInputDto>(ViewModel);

                var created = await pageAdminAppService.CreateAsync(createInput);

                return new OkObjectResult(created);
            }
            catch (System.Exception ex)
            {
                TempData.Add("MSG", ex.Message);
                return RedirectToPage();
            }
        }

        //[AutoMap(typeof(CreatePageInputDto), ReverseMap = true)]
        public class CreatePageViewModel
        {
            [Required]
            [DynamicMaxLength(typeof(PageConsts), nameof(PageConsts.MaxTitleLength))]
            public string Title { get; set; }

            [Required]
            [DynamicMaxLength(typeof(PageConsts), nameof(PageConsts.MaxSlugLength))]
            public string Slug { get; set; }

            [TextArea(Rows = 10)]
            [DynamicMaxLength(typeof(PageConsts), nameof(PageConsts.MaxContentLength))]
            public string Content { get; set; }

            [TextArea(Rows = 6)]
            [DynamicMaxLength(typeof(PageConsts), nameof(PageConsts.MaxScriptLength))]
            public string Script { get; set; }

            [TextArea(Rows = 6)]
            [DynamicMaxLength(typeof(PageConsts), nameof(PageConsts.MaxStyleLength))]
            public string Style { get; set; }
        }
    }
}

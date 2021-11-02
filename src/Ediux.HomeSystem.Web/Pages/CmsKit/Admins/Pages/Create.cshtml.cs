using AutoMapper;

using Ediux.HomeSystem.Miscellaneous;
using Ediux.HomeSystem.Models.DTOs.AutoSave;

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
        protected readonly IMiscellaneousAppService miscellaneousAppService;
        [BindProperty]
        public CreatePageViewModel ViewModel { get; set; }

        public CreateModel(IPageAdminAppService pageAdminAppService, IMiscellaneousAppService miscellaneousAppService)
        {
            this.pageAdminAppService = pageAdminAppService;
            this.miscellaneousAppService = miscellaneousAppService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var createInput = ObjectMapper.Map<CreatePageViewModel, CreatePageInputDto>(ViewModel);

                var created = await pageAdminAppService.CreateAsync(createInput);

                await miscellaneousAppService.RemoveAutoSaveDataAsync(new AutoSaveDTO()
                {
                    Id = CurrentUser.Id.ToString(),
                    entityType = "page"
                });

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

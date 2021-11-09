using AutoMapper;

using Ediux.HomeSystem.Miscellaneous;
using Ediux.HomeSystem.Models.DTOs.AutoSave;

using Microsoft.AspNetCore.Mvc;

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Validation;
using Volo.CmsKit.Admin.Pages;
using Volo.CmsKit.Pages;

namespace Ediux.HomeSystem.Web.Pages.CmsKit.Admins.Pages
{
    public class UpdateModel : CmsKitAdminPageModel
    {
        [BindProperty(SupportsGet = true)]
        [HiddenInput]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdatePageViewModel ViewModel { get; set; }

        protected readonly IPageAdminAppService pageAdminAppService;
        protected readonly IMiscellaneousAppService miscellaneousAppService;
        public UpdateModel(IPageAdminAppService pageAdminAppService,
            IMiscellaneousAppService miscellaneousAppService)
        {
            this.pageAdminAppService = pageAdminAppService;
            this.miscellaneousAppService = miscellaneousAppService;
        }

        public async Task OnGetAsync()
        {
            var dto = await pageAdminAppService.GetAsync(Id);

            ViewModel = ObjectMapper.Map<PageDto, UpdatePageViewModel>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var updateInput = ObjectMapper.Map<UpdatePageViewModel, UpdatePageInputDto>(ViewModel);

            await pageAdminAppService.UpdateAsync(Id, updateInput);
            await miscellaneousAppService.RemoveAutoSaveDataAsync(new AutoSaveDTO()
            {
                Id = CurrentUser.Id.ToString(),
                entityType = "page"
            });
            return new OkObjectResult(ViewModel);
        }

        [AutoMap(typeof(PageDto))]
        [AutoMap(typeof(UpdatePageInputDto), ReverseMap = true)]
        public class UpdatePageViewModel
        {
            [Required]
            [DynamicMaxLength(typeof(PageConsts), nameof(PageConsts.MaxTitleLength))]
            public string Title { get; set; }

            [Required]
            [DynamicMaxLength(typeof(PageConsts), nameof(PageConsts.MaxSlugLength))]
            public string Slug { get; set; }

            [TextArea(Rows = 6)]
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

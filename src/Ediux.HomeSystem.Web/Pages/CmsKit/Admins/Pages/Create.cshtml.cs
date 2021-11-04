using AutoMapper;

using Ediux.HomeSystem.Miscellaneous;
using Ediux.HomeSystem.Models.DTOs.AutoSave;

using Microsoft.AspNetCore.Mvc;

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        
        public async Task<IActionResult> OnGetAsync(Guid? Id)
        {
            if (Id.HasValue)
            {
                PageDto created = await pageAdminAppService.GetAsync(Id.Value);
                ViewModel = ObjectMapper.Map<PageDto, CreatePageViewModel>(created);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    PageDto created = null;

                    if (ViewModel.Id.HasValue)
                    {
                        created = await pageAdminAppService.GetAsync(ViewModel.Id.Value);
                    }
                    else
                    {
                        created = (await pageAdminAppService.GetListAsync(new GetPagesInputDto())).Items.SingleOrDefault(a => a.Slug == ViewModel.Slug);
                    }

                    if (created != null)
                    {
                        if (ViewModel.Id.HasValue == false)
                        {
                            ViewModel.Id = created.Id;
                        }
                        
                        created = await pageAdminAppService.UpdateAsync(ViewModel.Id.Value, ObjectMapper.Map<CreatePageViewModel, UpdatePageInputDto>(ViewModel));
                    }
                    else
                    {
                        var createInput = ObjectMapper.Map<CreatePageViewModel, CreatePageInputDto>(ViewModel);
                        created = await pageAdminAppService.CreateAsync(createInput);
                    }

                    ViewModel = ObjectMapper.Map<PageDto, CreatePageViewModel>(created);
                }

                return new OkObjectResult(ViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = new { message = ex.Message } });
            }
        }

        //[AutoMap(typeof(CreatePageInputDto), ReverseMap = true)]
        [AutoMap(typeof(UpdatePageInputDto), ReverseMap = true)]
        [AutoMap(typeof(PageDto), ReverseMap = true)]
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

            [HiddenInput]
            public Guid? Id { get; set; }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.CmsKit.Public.Tags;
using Volo.CmsKit.Tags;

namespace Ediux.HomeSystem.Web.Pages.Components.Tags
{
    [Widget(
        StyleFiles = new[]
        {
            "/Components/CmsKit/Tags/default.css"
        })]
    public class TagViewComponent : AbpViewComponent
    {
        protected readonly ITagAppService TagAppService;

        public TagViewComponent(ITagAppService tagAppService)
        {
            TagAppService = tagAppService;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync(
            string entityType,
            string entityId)
        {
            var tagDtos = await TagAppService.GetAllRelatedTagsAsync(entityType, entityId);

            var viewModel = new TagViewModel
            {
                EntityId = entityId,
                EntityType = entityType,
                Tags = tagDtos
            };

            return View("~/Components/CmsKit/Tags/Default.cshtml", viewModel);
        }

        public class TagViewModel
        {
            public List<TagDto> Tags { get; set; }
            public string EntityId { get; set; }
            public string EntityType { get; set; }
        }
    }
}

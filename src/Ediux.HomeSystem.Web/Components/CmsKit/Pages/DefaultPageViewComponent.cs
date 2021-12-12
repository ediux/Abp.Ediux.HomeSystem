using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.CmsKit.Public.Pages;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.HighlightJs;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace Ediux.HomeSystem.Web.Pages.Components.Pages
{
    [Widget(
        StyleTypes = new[]
        {
            typeof(HighlightJsStyleContributor)
        },
        ScriptTypes = new[]
        {
            typeof(HighlightJsScriptContributor)
        },
        ScriptFiles = new[]
        {
            "/Pages/CmsKit/Public/highlightOnLoad.js"
        })]
    public class DefaultPageViewComponent : AbpViewComponent
    {
        protected IPagePublicAppService PagePublicAppService { get; }

        public DefaultPageViewComponent(IPagePublicAppService pagePublicAppService)
        {
            PagePublicAppService = pagePublicAppService;
        }

        public virtual IViewComponentResult Invoke(Guid pageId, string title, string content)
        {
            var model = new PageViewModel
            {
                Id = pageId,
                Title = title,
                Content = content
            };

            return View("~//Components/CmsKit/Pages/Default.cshtml", model);
        }
    }

    public class PageViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
﻿using Microsoft.AspNetCore.Mvc;

using Volo.Abp.AspNetCore.Mvc;
using Volo.CmsKit.Public.Blogs;

namespace Volo.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.Blogs.BlogPostComment
{
    [ViewComponent(Name = "CmsDefaultBlogPostComment")]
    public class DefaultBlogPostCommentViewComponent : AbpViewComponent
    {
        protected IBlogPostPublicAppService BlogPostPublicAppService;

        public DefaultBlogPostCommentViewComponent(IBlogPostPublicAppService blogPostPublicAppService)
        {
            BlogPostPublicAppService = blogPostPublicAppService;
        }

        public virtual IViewComponentResult Invoke(string entityType, string entityId)
        {
            return View(
                "~/Pages/Components/Blogs/BlogPostComment/Default.cshtml",
                new DefaultBlogPostCommentViewModel
                {
                    EntityType = entityType,
                    EntityId = entityId
                });
        }

        public class DefaultBlogPostCommentViewModel
        {
            public string EntityType { get; set; }
            public string EntityId { get; set; }
        }
    }
}
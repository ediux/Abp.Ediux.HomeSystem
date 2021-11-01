using Ediux.HomeSystem.Models.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Admin.Pages;

namespace Ediux.HomeSystem.Miscellaneous
{
    public class MiscellaneousAppService : ApplicationService, IMiscellaneousAppService
    {

        protected readonly IPageAdminAppService pageAdminAppService;
        protected readonly IBlogPostAdminAppService blogPostAdminAppService;
        public MiscellaneousAppService(IPageAdminAppService pageAdminAppService,
            IBlogPostAdminAppService blogPostAdminAppService)
        {
            this.pageAdminAppService = pageAdminAppService;
            this.blogPostAdminAppService = blogPostAdminAppService;
        }

        public async Task<string> AutoSaveAsync(AutoSaveModel input)
        {
            if (input.entityType.ToLowerInvariant() == "page")
            {
                if (!string.IsNullOrWhiteSpace(input.id))
                {
                    Guid pageid = Guid.Parse(input.id);
                    var data = await pageAdminAppService.GetAsync(pageid);
                    data.Content = input.data;
                    data.LastModificationTime = DateTime.UtcNow;
                    data.LastModifierId = CurrentUser.Id;
                    await pageAdminAppService.UpdateAsync(pageid, ObjectMapper.Map<PageDto, UpdatePageInputDto>(data));
                    return input.id;
                }
                else
                {
                    if(!string.IsNullOrWhiteSpace(input.title) && !string.IsNullOrWhiteSpace(input.slug))
                    {
                        var data = await pageAdminAppService.CreateAsync(new CreatePageInputDto()
                        {
                            Content = input.data,
                            Slug = input.slug,
                            Title = input.title,
                            Script = input.script,
                            Style = input.style
                        });

                        return data.Id.ToString();
                    }
                   
                }


            }

            if (input.entityType.ToLowerInvariant() == "blogpost")
            {
                if (!string.IsNullOrEmpty(input.id))
                {
                    Guid blogid = Guid.Parse(input.id);
                    var blogdata = await blogPostAdminAppService.GetAsync(blogid);
                    blogdata.Content = input.data;
                    blogdata.CoverImageMediaId = Guid.Parse(input.coverImageMediaId);
                    blogdata.LastModificationTime = DateTime.UtcNow;
                    blogdata.ShortDescription = input.shortDescription;
                    blogdata.Slug = input.slug;

                    await blogPostAdminAppService.UpdateAsync(blogid, ObjectMapper.Map<BlogPostDto, UpdateBlogPostDto>(blogdata));
                    return input.id;
                }
                else
                {
                    if(!string.IsNullOrWhiteSpace(input.refid) && !string.IsNullOrWhiteSpace(input.slug) && !string.IsNullOrWhiteSpace(input.title))
                    {
                        var blogdata = await blogPostAdminAppService.CreateAsync(new CreateBlogPostDto()
                        {
                            BlogId = Guid.Parse(input.refid),
                            Content = input.data,
                            CoverImageMediaId = (!string.IsNullOrWhiteSpace(input.coverImageMediaId) ? Guid.Parse(input.coverImageMediaId) : default(Guid?)),
                            ShortDescription = input.shortDescription,
                            Slug = input.slug,
                            Title = input.title
                        });

                        return blogdata.Id.ToString();
                    }
                    
                }
            }

            return input.id;
        }
    }
}

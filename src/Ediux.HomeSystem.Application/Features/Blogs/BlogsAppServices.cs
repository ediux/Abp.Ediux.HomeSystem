using Ediux.HomeSystem.Features.Blogs.DTOs;

using System;

using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class BlogsAppServices : HomeSystemCrudAppService<Blogs, BlogItemDto, Guid, BlogSearchRequestDto>, IBlogsAppServices
    {
        public BlogsAppServices(IRepository<Blogs, Guid> repository) : base(repository)
        {
        }
    }
}

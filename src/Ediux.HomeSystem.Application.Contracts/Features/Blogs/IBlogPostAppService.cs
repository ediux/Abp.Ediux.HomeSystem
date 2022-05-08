using Ediux.HomeSystem.Features.Blogs.DTOs;

using System;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem.Features.Blogs
{
    public interface IBlogPostAppService : ICrudAppService<BlogPostItemDto,Guid, BlogPostSearchRequestDto>
    {
        /// <summary>
        /// 列出所有符合條件的貼文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<BlogPostItemDto>> GetListByBlogAsync(BlogPostSearchRequestDto input);
    }
}

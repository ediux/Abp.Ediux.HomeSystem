using Ediux.HomeSystem.Features.Blogs.DTOs;

using System;
using System.Threading.Tasks;
using System.Linq;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class BlogsAppServices : HomeSystemCrudAppService<Blogs, BlogItemDto, Guid, BlogSearchRequestDto>, IBlogsAppServices
    {
        public BlogsAppServices(IRepository<Blogs, Guid> repository, IdentityUserManager identityUserManager) : base(repository, identityUserManager)
        {
        }

        public override async Task<PagedResultDto<BlogItemDto>> GetListAsync(BlogSearchRequestDto input)
        {
            var query = (await Repository.WithDetailsAsync(p => p.Posts))
                .WhereIf(input != null && !input.Search.IsNullOrWhiteSpace(), p => p.Slug == input.Search || p.Name.Contains(input.Search))
                .If(input != null && !input.Sorting.IsNullOrWhiteSpace(), p => p.OrderBy(input.Sorting));

            var output = new PagedResultDto<BlogItemDto>(query.LongCount(), await MapToGetListOutputDtosAsync(query.ToList()));

            await ApplyExtensibleFullAuditedUserInformationToDTOsAsync(output);

            return output;
        }
    }
}

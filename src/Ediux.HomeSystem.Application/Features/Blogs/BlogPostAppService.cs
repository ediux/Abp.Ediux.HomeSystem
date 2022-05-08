using Ediux.HomeSystem.Features.Blogs.DTOs;

using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class BlogPostAppService : HomeSystemCrudAppService<BlogPosts, BlogPostItemDto, Guid, BlogPostSearchRequestDto>, IBlogPostAppService
    {
        public BlogPostAppService(IRepository<BlogPosts, Guid> repository, IdentityUserManager identityUserManager) : base(repository, identityUserManager)
        {
        }

        public async Task<ListResultDto<BlogPostItemDto>> GetListByBlogAsync(BlogPostSearchRequestDto input)
        {
            var query = (await Repository.WithDetailsAsync(p => p.Author, p => p.Blog, p => p.CoverImageMedia))
                .Where(p => p.BlogId == input.BlogId)
                .WhereIf(!input.Search.IsNullOrWhiteSpace(), p => p.Title.Contains(input.Search) || p.ShortDescription.Contains(input.Search) || p.Content.Contains(input.Search))
                .WhereIf(!input.SearchBySLUG.IsNullOrWhiteSpace(), p => p.Slug == input.SearchBySLUG);

            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                query = query.OrderBy(input.Sorting);
            }

            var result = query.ToList();
            var output = new ListResultDto<BlogPostItemDto>(await MapToGetListOutputDtosAsync(result.ToList()));
            await ApplyFullAuditedUserInformationToDTOsAsync(output);
            return output;
        }

        public override async Task<BlogPostItemDto> CreateAsync(BlogPostItemDto input)
        {
            var entity = await MapToEntityAsync(input);

            if (entity.Blog != null)
            {
                entity.Blog = null;
            }

            entity = await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToGetOutputDto(entity);

        }

        public override async Task<BlogPostItemDto> UpdateAsync(Guid id, BlogPostItemDto input)
        {
            var entity = await Repository.FindAsync(id, false);

            if (entity != null)
            {
                var updated = await MapToEntityAsync(input);
                bool hasChanged = false;

                if (entity.BlogId != updated.BlogId)
                {
                    entity.BlogId = updated.BlogId;
                    hasChanged = true;
                }

                if (entity.AuthorId != updated.AuthorId)
                {
                    entity.AuthorId = updated.AuthorId;
                    hasChanged = true;
                }

                if (entity.Content != updated.Content)
                {
                    entity.Content = updated.Content;
                    hasChanged = true;
                }

                if (entity.CoverImageMediaId != updated.CoverImageMediaId)
                {
                    entity.CoverImageMediaId = updated.CoverImageMediaId;
                    hasChanged = true;
                }

                if (entity.ShortDescription != updated.ShortDescription)
                {
                    entity.ShortDescription = updated.ShortDescription;
                    hasChanged = true;
                }

                if (entity.Slug != updated.Slug)
                {
                    entity.Slug = updated.Slug;
                    hasChanged = true;
                }

                if (entity.TenantId != updated.TenantId)
                {
                    entity.TenantId = updated.TenantId;
                    hasChanged = true;
                }

                if (hasChanged)
                {
                    entity.LastModificationTime = DateTime.Now;
                    entity.LastModifierId = CurrentUser.Id;

                    updated = await Repository.UpdateAsync(entity);
                    await CurrentUnitOfWork.SaveChangesAsync();

                    return MapToGetOutputDto(updated);
                }

                return input;
            }
            else
            {
                throw new AbpException(L[HomeSystemDomainErrorCodes.DataNotFound]);
            }
        }
    }
}

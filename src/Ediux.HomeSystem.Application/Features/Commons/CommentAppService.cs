using Ediux.HomeSystem.Features.Blogs;
using Ediux.HomeSystem.Features.Commons.DTOs;

using System;

using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.Features.Commons
{
    public class CommentAppService : HomeSystemCrudAppService<Comments, CommentDto, Guid, AbpSearchRequestDto>, ICommentAppService
    {
        public CommentAppService(IRepository<Comments, Guid> repository, IdentityUserManager identityUserManager) : base(repository, identityUserManager)
        {
        }
    }
}

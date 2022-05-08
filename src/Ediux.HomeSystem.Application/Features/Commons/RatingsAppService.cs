using Ediux.HomeSystem.Features.Blogs;
using Ediux.HomeSystem.Features.Commons.DTOs;

using System;

using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.Features.Commons
{
    public class RatingsAppService : HomeSystemCrudAppService<Ratings, RatingsDto, Guid, AbpSearchRequestDto>, IRatingsAppService
    {
        public RatingsAppService(IRepository<Ratings, Guid> repository, IdentityUserManager identityUserManager) : base(repository, identityUserManager)
        {
        }
    }
}

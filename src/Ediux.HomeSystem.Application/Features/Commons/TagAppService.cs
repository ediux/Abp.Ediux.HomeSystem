using Ediux.HomeSystem.Features.Blogs;
using Ediux.HomeSystem.Features.Commons.DTOs;

using System;

using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.Features.Commons
{
    public class TagAppService : HomeSystemCrudAppService<Tags, TagDto, Guid, AbpSearchRequestDto>, ITagAppService
    {
        private readonly IRepository<EntityTags> _entityTagsRepository;

        public TagAppService(IRepository<Tags, Guid> repository, IRepository<EntityTags> entityTagsRepository, IdentityUserManager identityUserManager) 
            : base(repository, identityUserManager)
        {
            _entityTagsRepository = entityTagsRepository;
        }
    }
}

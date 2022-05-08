using Ediux.HomeSystem.Features.Common;
using Ediux.HomeSystem.Features.Commons.DTOs;

using System;

using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.Features.Commons
{
    public class ConsortiumsAppService : HomeSystemCrudAppService<Consortiums, ConsortiumDto, Guid, AbpSearchRequestDto>, IConsortiumsAppService
    {
        public ConsortiumsAppService(IRepository<Consortiums, Guid> repository, IdentityUserManager identityUserManager) : base(repository, identityUserManager)
        {
        }
    }
}

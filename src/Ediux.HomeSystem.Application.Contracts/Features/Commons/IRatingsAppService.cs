using Ediux.HomeSystem.Features.Commons.DTOs;

using System;

using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem.Features.Commons
{
    public interface IRatingsAppService : ICrudAppService<RatingsDto, Guid, AbpSearchRequestDto>
    {
    }
}

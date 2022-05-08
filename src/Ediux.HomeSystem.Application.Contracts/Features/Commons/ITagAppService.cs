using Ediux.HomeSystem.Features.Commons.DTOs;

using System;

using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem.Features.Commons
{
    public interface ITagAppService : ICrudAppService<TagDto, Guid, AbpSearchRequestDto>
    {
    }
}

using System;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public interface IProductKeysBookAppService : ICrudAppService<ProductKeysBookDto, Guid, AbpSearchRequestDto>, ITransientDependency
    {
    }
}

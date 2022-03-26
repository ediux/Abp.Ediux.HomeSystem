
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public interface IPasswordStoreAppService : ICrudAppService<PasswordStoreDto, long, AbpSearchRequestDto>, ITransientDependency
    {
    }
}

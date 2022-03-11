using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    public interface IMIMETypeManagerAppService : ICrudAppService<MIMETypesDto, int, AbpSearchRequestDto>, ITransientDependency
    {
        Task<MIMETypesDto> GetByExtNameAsync(string ExtName);
    }
}

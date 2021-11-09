using System.Collections.Generic;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Miscellaneous
{
    public interface IComponentsManagementAppService : IApplicationService, ITransientDependency
    {
        Task CreateComponentsAsync(string Input);

        Task<List<string>> GetComponentsAsync();

        Task RemoveComponentAsync(string input);
    }
}

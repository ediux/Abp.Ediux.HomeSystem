using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem.Plugins.HololivePages.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}

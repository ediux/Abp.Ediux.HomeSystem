using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Ediux.ABP.Features.SmartAdminUI.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}

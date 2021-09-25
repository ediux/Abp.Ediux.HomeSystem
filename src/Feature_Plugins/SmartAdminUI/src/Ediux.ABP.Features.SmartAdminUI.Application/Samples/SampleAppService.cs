using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Ediux.ABP.Features.SmartAdminUI.Samples
{
    public class SampleAppService : SmartAdminUIAppService, ISampleAppService
    {
        public Task<SampleDto> GetAsync()
        {
            return Task.FromResult(
                new SampleDto
                {
                    Value = 42
                }
            );
        }

        [Authorize]
        public Task<SampleDto> GetAuthorizedAsync()
        {
            return Task.FromResult(
                new SampleDto
                {
                    Value = 42
                }
            );
        }
    }
}
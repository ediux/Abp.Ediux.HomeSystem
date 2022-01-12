using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Ediux.HomeSystem.Plugins.HololivePages.Samples
{
    public class SampleAppService : HololivePagesAppService, ISampleAppService
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
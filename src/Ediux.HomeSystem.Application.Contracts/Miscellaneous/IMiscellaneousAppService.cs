using Ediux.HomeSystem.Models.Views;

using System.Threading.Tasks;

using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem.Miscellaneous
{
    public interface IMiscellaneousAppService : IApplicationService
    {
        public Task<string> AutoSaveAsync(AutoSaveModel input);
    }
}

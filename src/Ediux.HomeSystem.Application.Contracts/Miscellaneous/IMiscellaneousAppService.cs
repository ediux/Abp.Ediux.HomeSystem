using Ediux.HomeSystem.Models.Views;

using System.Threading.Tasks;

using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem.Miscellaneous
{
    public interface IMiscellaneousAppService : IApplicationService
    {
        public Task<AutoSaveModel> AutoSaveAsync(AutoSaveModel input);

        public Task RemoveAutoSaveDataAsync(AutoSaveModel input);
    }
}

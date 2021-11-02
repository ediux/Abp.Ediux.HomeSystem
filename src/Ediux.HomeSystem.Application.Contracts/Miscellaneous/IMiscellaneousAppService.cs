using Ediux.HomeSystem.Models.DTOs.AutoSave;

using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Miscellaneous
{
    public interface IMiscellaneousAppService : IApplicationService, ITransientDependency
    {
        public Task<AutoSaveDTO> CreateAsync(AutoSaveDTO input);

        public Task RemoveAutoSaveDataAsync(AutoSaveDTO input);
    }
}

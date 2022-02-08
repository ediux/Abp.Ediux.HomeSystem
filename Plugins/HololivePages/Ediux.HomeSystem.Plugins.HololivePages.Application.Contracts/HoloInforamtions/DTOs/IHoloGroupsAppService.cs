using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions
{
    public interface IHoloGroupsAppService : ICrudAppService<HoloGroupDTO, Guid, HoloRequestDto>, ITransientDependency
    {
    }
}

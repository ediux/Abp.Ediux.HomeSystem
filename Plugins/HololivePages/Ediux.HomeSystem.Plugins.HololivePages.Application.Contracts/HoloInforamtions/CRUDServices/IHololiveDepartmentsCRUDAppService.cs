using System;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions.CRUDServices
{
    public interface IHololiveDepartmentsCRUDAppService : ICrudAppService<HoloDepartmentsDto, Guid, PagedAndSortedResultRequestDto>, ITransientDependency
    {
    }
}

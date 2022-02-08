using Ediux.HomeSystem.Plugins.HololivePages.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions.CRUDServices
{
    public class HololiveGroupCRUDAppService : CrudAppService<Groups, HoloGroupDTO, Guid, PagedAndSortedResultRequestDto>, IHololiveGroupCRUDAppService
    {
        public HololiveGroupCRUDAppService(IRepository<Groups, Guid> repository) : base(repository)
        {
        }

        public override Task<PagedResultDto<HoloGroupDTO>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }
    }
}

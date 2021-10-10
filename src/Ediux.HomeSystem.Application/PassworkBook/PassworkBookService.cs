using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.PassworkBook;

using Microsoft.AspNetCore.Authorization;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.PassworkBook
{
    
    public class PassworkBookService : CrudAppService<UserPasswordStore, PassworkBookDTO, long, jqDTSearchedResultRequestDto>, IPassworkBookService
    {
        private readonly IAuthorizationService authorizationService;

        public PassworkBookService(IRepository<UserPasswordStore, long> repository, IAuthorizationService authorizationService) : base(repository)
        {
            this.authorizationService = authorizationService;            
        }

        public async override Task<PagedResultDto<PassworkBookDTO>> GetListAsync(jqDTSearchedResultRequestDto input)
        {
            var result = await MapToGetListOutputDtosAsync(((await Repository.GetQueryableAsync())
                    .WhereIf(CurrentUser.IsAuthenticated, p => p.CreatorId == CurrentUser.Id)
                    .WhereIf(!string.IsNullOrEmpty(input.Search), p => p.Account.Contains(input.Search) || p.SiteName.Contains(input.Search) || p.Site.Contains(input.Search))
                    .Distinct()
                    .OrderBy(o=>o.SiteName)
                    .ThenBy(o=>o.CreationTime)
                    .ToList()));

            return new PagedResultDto<PassworkBookDTO>(result.LongCount(), result);

        }
    }
}

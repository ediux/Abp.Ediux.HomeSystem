using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.PassworkBook;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.FeatureManagement;

namespace Ediux.HomeSystem.PassworkBook
{
    public class PassworkBookService : CrudAppService<UserPasswordStore, PassworkBookDTO, long>, IPassworkBookService
    {
        private readonly IAuthorizationService authorizationService;

        public PassworkBookService(IRepository<UserPasswordStore, long> repository, IAuthorizationService authorizationService) : base(repository)
        {
            this.authorizationService = authorizationService;
        }

        public async override Task<PagedResultDto<PassworkBookDTO>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var result = await MapToGetListOutputDtosAsync(((await Repository.GetQueryableAsync())
                    .WhereIf(true, p => p.CreatorId == CurrentUser.Id)
                    .Distinct()
                    .ToList()));

            return new PagedResultDto<PassworkBookDTO>(result.LongCount(), result);

        }
    }
}

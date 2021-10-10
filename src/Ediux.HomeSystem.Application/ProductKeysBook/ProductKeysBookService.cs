using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;

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
using Volo.Abp.Users;

namespace Ediux.HomeSystem.ProductKeysBook
{
    public class ProductKeysBookService : CrudAppService<ProductKeys, ProductKeysBookDTO, Guid, jqDTSearchedResultRequestDto>, IProductKeysBookService
    {
        private readonly IAuthorizationService authorizationService;

        public ProductKeysBookService(IRepository<ProductKeys, Guid> repository, IAuthorizationService authorizationService) : base(repository)
        {
            this.authorizationService = authorizationService;
        }

        public async override Task<PagedResultDto<ProductKeysBookDTO>> GetListAsync(jqDTSearchedResultRequestDto input)
        {
            var grantResult = await authorizationService.AuthorizeAsync(FeatureManagementPermissions.ManageHostFeatures);

            var result = (await Repository.GetQueryableAsync())
                .WhereIf(grantResult.Succeeded == false, p => p.CreatorId == CurrentUser.Id)
                .If(grantResult.Succeeded==false, async o=>o.Union((await Repository.GetQueryableAsync()).WhereIf(grantResult.Succeeded == false, p => p.Shared == true && p.CreatorId != CurrentUser.Id)))                
                .Distinct()
                .ToList();

            return new PagedResultDto<ProductKeysBookDTO>(result.LongCount(), await MapToGetListOutputDtosAsync(result));
        }       
    }
}

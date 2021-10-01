using Ediux.HomeSystem.Data;
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
    public class ProductKeysBookService : CrudAppService<ProductKeys, ProductKeysBookDTO, Guid>, IProductKeysBookService
    {
        private readonly IAuthorizationService authorizationService;

        public ProductKeysBookService(IRepository<ProductKeys, Guid> repository, IAuthorizationService authorizationService) : base(repository)
        {
            this.authorizationService = authorizationService;
        }

        public async override Task<PagedResultDto<ProductKeysBookDTO>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var grantResult = await authorizationService.AuthorizeAsync(FeatureManagementPermissions.ManageHostFeatures);

            if (grantResult.Succeeded)
            {
                //擁有全系統管理權限可以看到全系統紀錄
                var result_all = await MapToGetListOutputDtosAsync((await Repository.GetQueryableAsync()).Distinct().ToList());
                return new PagedResultDto<ProductKeysBookDTO>(result_all.LongCount(), result_all);
            }
            else
            {                
                //只能看到自己或其他人分享的金鑰紀錄
                var result = await MapToGetListOutputDtosAsync(((await Repository.GetQueryableAsync()).Where(p => p.CreatorId == CurrentUser.Id)
                               .Union((await Repository.GetQueryableAsync()).Where(p => p.Shared == true && p.CreatorId != CurrentUser.Id)).Distinct()
                               .ToList()));

                return new PagedResultDto<ProductKeysBookDTO>(result.LongCount(), result);
            }
        }       
    }
}

using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.FeatureManagement;
using Volo.Abp.ObjectExtending;

namespace Ediux.HomeSystem.ProductKeysBook
{
    public class ProductKeysBookService : CrudAppService<ProductKeys, ProductKeysBookDTO, Guid, jqDTSearchedResultRequestDto>, IProductKeysBookService
    {
        private readonly IAuthorizationService authorizationService;

        public ProductKeysBookService(IRepository<ProductKeys, Guid> repository, IAuthorizationService authorizationService) : base(repository)
        {
            this.authorizationService = authorizationService;
        }

        public async override Task<ProductKeysBookDTO> CreateAsync(ProductKeysBookDTO input)
        {
            var entity = MapToEntity(input);
            input.MapExtraPropertiesTo(entity, MappingPropertyDefinitionChecks.None);
            return await MapToGetOutputDtoAsync(await Repository.InsertAsync(entity));
        }

        public async override Task<ProductKeysBookDTO> UpdateAsync(Guid id, ProductKeysBookDTO input)
        {
            var entity = await Repository.FindAsync(id, includeDetails: true);// MapToEntity(input);

            if (entity == null) return input;

            var updated = await MapToEntityAsync(input);

            

            input.MapExtraPropertiesTo(updated, MappingPropertyDefinitionChecks.None);

            var removeExtraProperties = entity.ExtraProperties.Keys.Except(updated.ExtraProperties.Keys);

            if (removeExtraProperties.Any())
            {
                foreach(var removeKey in removeExtraProperties)
                {
                    entity.RemoveProperty(removeKey);
                }
            }
            
            updated.MapExtraPropertiesTo(entity, MappingPropertyDefinitionChecks.None);

            if (entity.ProductKey != updated.ProductKey)
            {
                entity.ProductKey = updated.ProductKey;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            if (entity.ProductName != updated.ProductName)
            {
                entity.ProductName = updated.ProductName;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            if (entity.Shared != updated.Shared)
            {
                entity.Shared = updated.Shared;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            return await MapToGetOutputDtoAsync(await Repository.UpdateAsync(entity));
        }
        public async override Task<PagedResultDto<ProductKeysBookDTO>> GetListAsync(jqDTSearchedResultRequestDto input)
        {

            var grantResult = await authorizationService.AuthorizeAsync(FeatureManagementPermissions.ManageHostFeatures);

            var result = (await Repository.GetQueryableAsync())
                .WhereIf(grantResult.Succeeded == false, p => p.CreatorId == CurrentUser.Id)
                .If(grantResult.Succeeded == false, async o => o.Union((await Repository.GetQueryableAsync())
                  .WhereIf(grantResult.Succeeded == false, p => p.Shared == true && p.CreatorId != CurrentUser.Id)))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Search), p => p.ProductKey.Contains(input.Search) || p.ProductName.Contains(input.Search) || p.ProductName.Contains(input.Search))
                .Distinct()                
                .ToList();

            var output = result.Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            return new PagedResultDto<ProductKeysBookDTO>(result.LongCount(), (await MapToGetListOutputDtosAsync(output)));
        }
    }
}

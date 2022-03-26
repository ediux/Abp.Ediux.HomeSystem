
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.SystemManagement
{
    public class MIMETypeManagerAppService : CrudAppService<MIMEType, MIMETypesDto, int, AbpSearchRequestDto>, IMIMETypeManagerAppService
    {
        public MIMETypeManagerAppService(IRepository<MIMEType, int> repository) : base(repository)
        {

        }

        public async Task<MIMETypesDto> GetByExtNameAsync(string ExtName)
        {
            var result = (await this.Repository.GetQueryableAsync())
                .Where(w => w.RefenceExtName == ExtName)
                .SingleOrDefault();

            if (result != null)
                return MapToGetOutputDto(result);
            else
                return null;
        }

        public async override Task<PagedResultDto<MIMETypesDto>> GetListAsync(AbpSearchRequestDto input)
        {
            var result = (await this.Repository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Search), p => p.TypeName.Contains(input.Search) || p.RefenceExtName.Contains(input.Search));

            var output = result
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();

            return new PagedResultDto<MIMETypesDto>(result.Count(), await MapToGetListOutputDtosAsync(output));
        }


    }
}

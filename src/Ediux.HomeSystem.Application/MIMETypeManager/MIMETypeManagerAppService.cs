using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.MIMETypes;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.MIMETypeManager
{
    public class MIMETypeManagerAppService : CrudAppService<MIMEType, MIMETypesDTO, int, jqDTSearchedResultRequestDto>, IMIMETypeManagerAppService
    {
        public MIMETypeManagerAppService(IRepository<MIMEType, int> repository) : base(repository)
        {

        }

        public async Task<MIMETypesDTO> GetByExtNameAsync(string ExtName)
        {
            var result = (await this.Repository.GetQueryableAsync())
                .Where(w => w.RefenceExtName == ExtName)
                .SingleOrDefault();

            if (result != null)
                return MapToGetOutputDto(result);
            else
                return null;
        }

        public async override Task<PagedResultDto<MIMETypesDTO>> GetListAsync(jqDTSearchedResultRequestDto input)
        {
            var result = (await this.Repository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Search), p => p.MIME.Contains(input.Search) || p.RefenceExtName.Contains(input.Search));

            var output = result
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();

            return new PagedResultDto<MIMETypesDTO>(result.Count(), await MapToGetListOutputDtosAsync(output));
        }


    }
}

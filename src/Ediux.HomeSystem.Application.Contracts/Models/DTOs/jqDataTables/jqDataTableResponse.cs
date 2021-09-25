using Newtonsoft.Json;

using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace Ediux.HomeSystem.Models.jqDataTables
{
    public class jqDataTableResponse<TModel> : jqDataTables<TModel>
    {
        public jqDataTableResponse(jqDataTableRequest request, IPagedResult<TModel> query)
            : base(request, query)
        {
        }
      
        //public jqDataTableResponse(jqDataTableRequest request, IPagedResult<TModel> query,
        //    IEnumerable<string> includes = null,
        //    Func<IQueryable<TModel>, IEnumerable<TModel>> conversion = null,
        //    bool canUseAutoMapperProjection = true)
        //    : base(request, query, includes, conversion, canUseAutoMapperProjection)
        //{
        //}

        //public jqDataTableResponse(jqDataTableRequest request, IPagedResult<TModel> source)
        //    : this(request, source.AsQueryable())
        //{
        //}

        //public jqDataTableResponse(jqDataTableRequest request, IPagedResult<TModel> source,
        //    IEnumerable<string> includes = null,
        //    Func<IPagedResult<TModel>, IEnumerable<TModel>> conversion = null,
        //    bool canUseAutoMapperProjection = true)
        //    : base(request, source.AsQueryable(), includes, conversion, canUseAutoMapperProjection)
        //{
        //}

        //public jqDataTableResponse(IPagedResult<TModel> list, int totalCount)
        //    : base(list, totalCount)
        //{
        //}
    }
}


using Volo.Abp.Application.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json.Serialization;

namespace Ediux.HomeSystem.Models.jqDataTables
{
    public abstract class jqDataTables<TViewModel> : IPagedResult<TViewModel>
    {
        private readonly IQueryable<TViewModel> _query;

        ///// <summary>
        ///// Response to the draw from DataTablesParameters, always cast the parameter value to int before returning it to prevent XSS
        ///// </summary>
        //public int draw { get; set; }


        /// <summary>
        /// Total records after filtering (not just the number returned from this page of data)
        /// </summary>
        public int recordsFiltered { get; set; }


        /// <summary>
        /// Optional, in the event an error was detected but was found to be recoverable
        /// </summary>
        public string error { get; set; }

        /// <summary>
        /// Data to display in table
        /// </summary>
        [JsonPropertyName("data")]
        public virtual IReadOnlyList<TViewModel> Items { get; set; }


        /// <summary>
        /// Total records prior to filtering (in DB)
        /// </summary>
        [JsonPropertyName("recordsTotal")]
        public virtual long TotalCount { get; set; }


        public jqDataTables(jqDataTableRequest request,
            IPagedResult<TViewModel> query)
        {
            //_conversion = conversion ?? GetAutoMapperConversion(canUseAutoMapperProjection);

            IList<string> includesAsList = null;

            if (request != null)
            {
                includesAsList = request.order.Select(s=>s.columnName).ToList();
            }

            if (query != null)
            {
                _query = query.Items.AsQueryable();
                var count = TotalCount = recordsFiltered = query.Items.Count;

                var tempQuery = ApplySorting(query.Items, request.columns, request.order);

                // Paging
                if (request.start.HasValue && request.start > 0)
                {
                    tempQuery = tempQuery.Skip(request.start.Value);
                }
                if (request.length.HasValue && request.length > 0)
                {
                    tempQuery = tempQuery.Take(request.length.Value);
                }

                Items = tempQuery.ToList();
            }
            else
            {
                TotalCount = 0;
                Items = Enumerable.Empty<TViewModel>().ToList();
            }
            
            //Groups = null;
        }

        //protected jqDataTables(jqDataTableRequest request, IPagedResult<TViewModel> entities)
        //        : this(request, entities)
        //{
        //}

        //protected jqDataTables(IPagedResult<TViewModel> list, int totalCount)
        //{
        //    Items = list;
        //    TotalCount = totalCount;
        //}

        public IQueryable<TViewModel> AsQueryable()
        {
            return _query;
        }

        private IQueryable<TViewModel> ApplySorting(IReadOnlyList<TViewModel> query, IEnumerable<jqDataTableColumn> columns, IEnumerable<jqDataTableOrder> order)
        {
            string sorting = GetSorting(columns, order) ?? typeof(TViewModel).FirstSortableProperty();
            return query.AsQueryable().OrderBy(sorting);
        }

        protected string GetSorting(IEnumerable<jqDataTableColumn> columns, IEnumerable<jqDataTableOrder> orders)
        {
            if (orders == null)
            {
                return null;
            }

            foreach (var order in orders)
            {
                order.columnName = columns.ElementAt(order.column).data;
            }

            var expression = string.Join(",", orders.Select(s => s.columnName + " " + s.dir));
            return expression.Length > 1 ? expression : null;
        }

        //public static Func<IPagedResult<TViewModel>, IReadOnlyList<TViewModel>> GetAutoMapperConversion(bool canUseAutoMapperProjection = true)
        //{
        //    Func<IQueryable<TViewModel>, IEnumerable<TViewModel>> conversion;
        //    MapperConfiguration mapperConfiguration = new MapperConfiguration(a => { });

        //    if (TypeExtensions.SameTypes<TViewModel, TViewModel>())
        //    {
        //        conversion = q => q.Cast<TViewModel>().ToList();
        //    }
        //    else
        //    {
        //        // https://github.com/AutoMapper/AutoMapper/issues/362
        //        // The idea behind Project().To is to be passed to a query provider like EF or NHibernate that will then do the appropriate SQL creation, 
        //        // not necessarily that the in-memory-execution will work.
        //        // Project.To has a TON of limitations as it's built explicitly for real query providers, and only does things like MapFrom etc.
        //        // To put it another way - don't use Project.To unless you're passing that to EF or NH or another DB query provider that knows what to do with the expression tree.
        //        if (canUseAutoMapperProjection)
        //        {
        //            conversion = q => q.ProjectTo<TViewModel>(mapperConfiguration).ToList();
        //        }
        //        else
        //        {
        //            IMapper mapper = mapperConfiguration.CreateMapper();

        //            conversion = mapper.Map<IEnumerable<TViewModel>>;
        //        }
        //    }

        //    return conversion;
        //}
    }
}

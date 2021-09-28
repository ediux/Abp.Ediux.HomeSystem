using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;
using Ediux.HomeSystem.ProductKeysBook;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.EndPoints
{
    public class ProductKeysBookEndPoint : jqDataTableEndpointBase<IProductKeysBookService, ProductKeysBookDTO, Guid, ProductKeysBookDTO, ProductKeysBookDTO>
    {
        public ProductKeysBookEndPoint(IProductKeysBookService crudAppService) : base(crudAppService)
        {
        }
    }
}

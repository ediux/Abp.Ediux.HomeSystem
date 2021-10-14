using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;
using Ediux.HomeSystem.Models.jqDataTables;
using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.ProductKeysBook;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Guids;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.EndPoints
{
    [Authorize(HomeSystemPermissions.ProductKeysBook.Execute)]
    [Route("api/productkeysbook")]
    public class ProductKeysBookEndPoint : jqDataTableEndpointBase<IProductKeysBookService, ProductKeysBookDTO, Guid, ProductKeysBookDTO, ProductKeysBookDTO>
    {
        private readonly ICurrentUser currentUser;
        private readonly IGuidGenerator guidGenerator;

        public ProductKeysBookEndPoint(IProductKeysBookService crudAppService,ICurrentUser currentUser,IGuidGenerator guidGenerator) : base(crudAppService)
        {
            this.currentUser = currentUser;
            this.guidGenerator = guidGenerator;
        }

        public async override Task<IActionResult> Create([FromForm] ProductKeysBookDTO input)
        {
            try
            {
                input.Id = guidGenerator.Create();
                return await base.Create(input);
            }
            catch (Exception ex)
            {
                return BadRequest(new jqDataTableResponse<ProductKeysBookDTO>(null, null) { error = ex.Message });                
            }
       
        }
    }
}

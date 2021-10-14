using Ediux.HomeSystem.Models.DTOs.PassworkBook;
using Ediux.HomeSystem.PassworkBook;
using Ediux.HomeSystem.Permissions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.EndPoints
{
    [Authorize(HomeSystemPermissions.PasswordBook.Execute)]
    [Route("api/passwordbook")]
    public class PassworkBookEndPoint : jqDataTableEndpointBase<IPassworkBookService, PassworkBookDTO, long, PassworkBookDTO, PassworkBookDTO>
    {
        public PassworkBookEndPoint(IPassworkBookService crudAppService) : base(crudAppService)
        {
        }
    }
}

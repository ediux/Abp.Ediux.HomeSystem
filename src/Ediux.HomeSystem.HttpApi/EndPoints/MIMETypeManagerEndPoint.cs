using Ediux.HomeSystem.MIMETypeManager;
using Ediux.HomeSystem.Models.DTOs.MIMETypes;
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
    [Authorize(HomeSystemPermissions.MIMETypeManager.Execute)]
    [Route("api/mimetypemanager")]
    public class MIMETypeManagerEndPoint : jqDataTableEndpointBase<IMIMETypeManagerAppService, MIMETypesDTO, int, MIMETypesDTO, MIMETypesDTO>
    {
       
        public MIMETypeManagerEndPoint(IMIMETypeManagerAppService crudAppService) : base(crudAppService)
        {
        }
    }
}

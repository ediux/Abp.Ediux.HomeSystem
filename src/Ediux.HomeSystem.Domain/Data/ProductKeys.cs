using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Data
{
    public class ProductKeys : AuditedEntity<Guid>, IAuditedObject
    {

        //[MaxLength(256)]
        //[Required]
        public string ProductName { get; set; }
        //[MaxLength(256)]
        //[Required]
        public string ProductKey { get; set; }

        //[Required]
        public bool Shared { get; set; }

    }
}

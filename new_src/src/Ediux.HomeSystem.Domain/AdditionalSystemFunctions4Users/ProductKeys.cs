using System;

using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public class ProductKeys : AuditedAggregateRoot<Guid>
    {
        public ProductKeys()
        {

        }

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

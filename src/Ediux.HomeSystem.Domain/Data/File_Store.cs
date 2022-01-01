using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Data
{
    public class File_Store : FullAuditedAggregateRoot<Guid>, IFullAuditedObject
    {
        //[Required]
        //[MaxLength(256)]
        public string Name { get; set; }
        public string ExtName { get; set; }

        public int MIMETypeId { get; set; }

        public long Size { get; set; }
        //[IgnoreMap]
        //public bool IsCrypto { get; set; }
        //[IgnoreMap]
        //public bool InRecycle { get; set; }
        public string OriginFullPath { get; set; }
        //[IgnoreMap]
        //public bool StorageInSMB { get; set; }
        //[IgnoreMap]
        //public string SMBFullPath { get; set; }
        //[IgnoreMap]
        //public string SMBLoginId { get; set; }
        //[IgnoreMap]
        //public string SMBPassword { get; set; }

        public virtual MIMEType MIME { get; set; }
    }
}

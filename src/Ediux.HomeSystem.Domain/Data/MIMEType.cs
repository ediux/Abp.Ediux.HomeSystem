using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Data
{
    public class MIMEType : AuditedEntity<int>, IAuditedObject
    {
        //[Required]
        //[MaxLength(256)]
        public string MIME { get; set; }

        /// <summary>
        /// 對應的附檔名
        /// </summary>
        //[MaxLength(256)]
        public string RefenceExtName { get; set; }

        /// <summary>
        /// 說明描述
        /// </summary>
        //[MaxLength]
        public string Description { get; set; }

        public virtual HashSet<File_Store> Files { get; set; }
    }
}

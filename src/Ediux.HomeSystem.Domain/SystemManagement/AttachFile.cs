using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Entities;

namespace Ediux.HomeSystem.SystemManagement
{
    public class AttachFile : Entity
    {

        /// <summary>
        /// 系統訊息識別碼
        /// </summary>
        public Guid SystemMessageId { get; set; }


        /// <summary>
        /// 參照的系統訊息
        /// </summary>
        public virtual InternalSystemMessages SystemMessage { get; set; }

        /// <summary>
        /// 參照檔案識別碼
        /// </summary>
        public Guid FileStoreId { get; set; }

        /// <summary>
        /// 附加檔案
        /// </summary>
        public virtual File_Store File { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { SystemMessageId, FileStoreId };
        }
    }
}

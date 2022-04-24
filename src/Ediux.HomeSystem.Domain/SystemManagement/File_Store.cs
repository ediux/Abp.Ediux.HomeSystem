
using Ediux.HomeSystem.Features.Common;

using System;
using System.Collections.Generic;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.SystemManagement
{
    public class File_Store : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 檔案名稱(不含附檔名)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 檔案類型識別碼
        /// </summary>
        public int MIMETypeId { get; set; }

        /// <summary>
        /// 檔案大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 檔案分類識別碼
        /// </summary>
        public Guid? FileClassificationId { get; set; }

        /// <summary>
        /// 檔案分類
        /// </summary>
        public virtual FileStoreClassification Classification { get; set; }

        /// <summary>
        /// 是否為公開檔案?
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Abp的Blob容器名稱
        /// </summary>
        public string BlobContainerName { get; set; }

        /// <summary>
        /// 檔案類型
        /// </summary>
        public virtual MIMEType MIME { get; set; }

        /// <summary>
        /// 關聯的外掛模組
        /// </summary>
        public virtual AbpPlugins Plugins { get; set; }

        /// <summary>
        /// 被參照的系統訊息
        /// </summary>
        public virtual ICollection<AttachFile> RefencedByMessages { get; set; }

        public virtual ICollection<Consortiums> Consortiums { get; set; }
        public File_Store()
        {
            RefencedByMessages = new HashSet<AttachFile>();
            Consortiums = new HashSet<Consortiums>();
        }
    }
}

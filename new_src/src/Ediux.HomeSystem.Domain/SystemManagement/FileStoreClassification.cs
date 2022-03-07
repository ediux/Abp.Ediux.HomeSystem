using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.SystemManagement
{
    public class FileStoreClassification : AuditedAggregateRoot<Guid>, IAuditedObject
    {
        /// <summary>
        /// 分類名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上層分類識別碼
        /// </summary>
        public Guid? ParentClassificationId { get; set; }

        /// <summary>
        /// 上層分類
        /// </summary>
        public virtual FileStoreClassification Parent { get; set; }

        /// <summary>
        /// 子分類
        /// </summary>
        public virtual ICollection<FileStoreClassification> Childs { get; set; }

        /// <summary>
        /// 在此分類下的檔案清單
        /// </summary>
        public virtual ICollection<File_Store> Files { get; set; }

        public FileStoreClassification()
        {
            Childs = new HashSet<FileStoreClassification>();
            Files = new HashSet<File_Store>();
        }
    }
}

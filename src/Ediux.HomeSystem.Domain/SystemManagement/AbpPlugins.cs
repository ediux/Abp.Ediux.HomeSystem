using System;
using System.Collections.Generic;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.SystemManagement
{
    /// <summary>
    /// 擴充模組管理資料表
    /// </summary>
    public class AbpPlugins : AuditedAggregateRoot<Guid>, IAuditedObject
    {
        /// <summary>
        /// 擴充模組套件名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 組件名稱
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 關聯的檔案識別碼
        /// </summary>
        public Guid? RefFileStoreId { get; set; }

        /// <summary>
        /// 關聯的檔案資訊物件
        /// </summary>
        public virtual File_Store RefFileInstance { get; set; }

        /// <summary>
        /// 啟用/停用
        /// </summary>
        public bool Disabled { get; set; }

        
        
        /// <summary>
        /// 相依的組件關聯
        /// </summary>
        public virtual ICollection<AbpPlugins> DependencyAssembly { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        public AbpPlugins()
        {
            DependencyAssembly = new HashSet<AbpPlugins>();
        }
    }
}

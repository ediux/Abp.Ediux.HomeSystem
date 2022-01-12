using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Plugins.HololivePages.Data
{
    /// <summary>
    /// 集團資訊
    /// </summary>
    public class Groups : AuditedEntity<Guid>
    {
        /// <summary>
        /// 集團名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 註冊日期
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// 集團負責人
        /// </summary>
        public string GroupRepresentative { get; set; }

        /// <summary>
        /// 集團簡述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 集團商標圖檔參考ID
        /// </summary>
        public Guid? LogoFileRef { get; set; }

        /// <summary>
        /// 集團旗下公司集合
        /// </summary>
        public virtual ICollection<Company> Companies { get; set; }

        public Groups()
        {
            Companies = new HashSet<Company>();
        }
    }
}

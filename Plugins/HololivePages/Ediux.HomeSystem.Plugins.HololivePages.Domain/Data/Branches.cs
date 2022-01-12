using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Plugins.HololivePages.Data
{
    /// <summary>
    /// 品牌資訊
    /// </summary>
    public class Branches : AuditedEntity<Guid>
    {
        /// <summary>
        /// 品牌名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 品牌創立時間
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 品牌結束時間
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 品牌簡述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 品牌商標圖檔參考ID
        /// </summary>
        public Guid? LogoFileRef { get; set; }

        /// <summary>
        /// 所屬公司參考ID
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 所屬公司
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// 組別
        /// </summary>
        public virtual ICollection<Departments> Departments { get; set; }

        public Branches()
        {
            Company = new Company();
            Departments = new HashSet<Departments>();
        }
    }
}

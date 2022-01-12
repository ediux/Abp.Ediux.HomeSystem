using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Plugins.HololivePages.Data
{
    public class Departments : AuditedEntity<Guid>
    {
        /// <summary>
        /// 所屬品牌ID
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// 所屬品牌
        /// </summary>
        public virtual Branches Branch { get; set; }

        /// <summary>
        /// 組別名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 組別簡述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 開始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 結束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Members> Members { get; set; }

        public Departments()
        {
            Members = new HashSet<Members>();
        }
    }
}

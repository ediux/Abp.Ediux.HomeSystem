using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Plugins.HololivePages.Data
{
    /// <summary>
    /// 定義Cover/Hololive 公司簡介的資訊
    /// </summary>
    public class Company : AuditedEntity<Guid>
    {
        /// <summary>
        /// 集團名稱
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// 所屬集團導覽屬性
        /// </summary>
        public virtual Groups Groups { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否為總公司
        /// </summary>
        public bool IsHQ { get; set; }

        /// <summary>
        /// 負責地區
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 註冊日期
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// 開始營運日期
        /// </summary>
        public DateTime? StartOperationDate { get; set; }

        /// <summary>
        /// 結束營運日期
        /// </summary>
        public DateTime? EndOperationDate { get; set; }

        /// <summary>
        /// 公司負責人
        /// </summary>
        public string CompanyRepresentative { get; set; }

        /// <summary>
        /// 公司執行長姓名
        /// </summary>
        public string CEO { get; set; }

        /// <summary>
        /// 公司簡述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 公司商標圖檔參考ID
        /// </summary>
        public Guid? LogoFileRef { get; set; }

        /// <summary>
        /// 旗下品牌
        /// </summary>
        public virtual ICollection<Branches> Branches { get; set; }

        public Company()
        {
            Branches = new HashSet<Branches>();
        }
    }
}

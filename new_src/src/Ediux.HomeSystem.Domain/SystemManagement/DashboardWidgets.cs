using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.SystemManagement
{
    /// <summary>
    /// 儀錶板Widget管理表
    /// </summary>
    public class DashboardWidgets : AuditedEntity<Guid>, IAuditedObject
    {
        /// <summary>
        /// Widget識別名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Widget顯示名稱
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 是否包含設定選項功能?
        /// </summary>
        public bool HasOption { get; set; }

        /// <summary>
        /// 是否允許多個相同Widget?
        /// </summary>
        public bool AllowMulti { get; set; }

        /// <summary>
        /// 是否為系統預設值?
        /// </summary>
        public bool IsDefault { get; set; }
 
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 指派的使用者清單
        /// </summary>
        public virtual ICollection<DashboardWidgetUsers> AssginedUsers { get; set; }

        public DashboardWidgets()
        {
            AssginedUsers = new Collection<DashboardWidgetUsers>();
        }
    }
}

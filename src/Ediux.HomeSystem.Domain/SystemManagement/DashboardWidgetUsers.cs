using System;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.SystemManagement
{
    /// <summary>
    /// 儀錶板使用者對應表
    /// </summary>
    public class DashboardWidgetUsers : Entity
    {
        public DashboardWidgetUsers()
        {

        }

        public DashboardWidgetUsers(Guid userId, Guid widgetId)
        {
            UserId = userId;
            DashboardWidgetId = widgetId;         
        }

        public Guid UserId { get; set; }

        public virtual IdentityUser User { get; set; }

        public Guid DashboardWidgetId { get; set; }

        public virtual DashboardWidgets DashboardWidget { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { UserId, DashboardWidgetId };
        }
    }
}

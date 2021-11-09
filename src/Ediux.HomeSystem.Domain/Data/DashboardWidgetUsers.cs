using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.Data
{
    public class DashboardWidgetUsers : AuditedEntity<Guid>, IAuditedObject
    {
        public DashboardWidgetUsers()
        {

        }

        public DashboardWidgetUsers(Guid userId, Guid widgetId)
        {
            Id = userId;
            DashboardWidgetId = widgetId;
            CreatorId = userId;
            
        }

        public Guid DashboardWidgetId { get; set; }

        public string UserSettings { get; set; }

        public virtual DashboardWidgets DashboardWidget { get; set; }
    }
}

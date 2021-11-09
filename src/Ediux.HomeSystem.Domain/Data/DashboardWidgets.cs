using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Data
{
    public class DashboardWidgets : AuditedEntity<Guid>, IAuditedObject
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool HasOption { get; set; }

        public bool AllowUserSetting { get; set; }

        public bool AllowMulti { get; set; }

        public bool IsDefault { get; set; }

        public string PermissionName { get; set; }

        public int Order { get; set; }

        [AutoMapper.IgnoreMap]
        public virtual ICollection<DashboardWidgetUsers> AssginedUsers { get; set; }

        public DashboardWidgets()
        {
            AssginedUsers = new Collection<DashboardWidgetUsers>();
        }
    }
}

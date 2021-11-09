using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Data
{
    public class ComponentsRegistration : AuditedEntity<Guid>, IAuditedObject
    {
        public string Name { get; set; }

        public string SettingName { get; set; }

        public string PermissionName { get; set; }

        public bool HasOption { get; set; }

        public bool AllowUserSetting { get; set; }
    }
}

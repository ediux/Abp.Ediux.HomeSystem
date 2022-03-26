
using Ediux.HomeSystem.SystemManagement;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public class DashBoardWidgetsDto : AuditedEntityDto<Guid>, IAuditedObject, ITransientDependency
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool HasOption { get; set; }

        public bool AllowUserSetting { get; set; }

        public bool AllowMulti { get; set; }

        public bool IsDefault { get; set; }

        public string PermissionName { get; set; }

        public int Order { get; set; }

        public virtual ICollection<UserInforamtionDto> Users { get; set; }

        public DashBoardWidgetsDto()
        {
            Users = new Collection<UserInforamtionDto>();
        }
    }
}

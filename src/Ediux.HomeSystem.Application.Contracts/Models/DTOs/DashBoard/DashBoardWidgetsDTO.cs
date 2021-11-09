using AutoMapper;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.DashBoard
{   
    public class DashBoardWidgetsDTO : AuditedEntityDto<Guid>, IAuditedObject, ITransientDependency
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool HasOption { get; set; }

        public bool AllowUserSetting { get; set; }

        public bool AllowMulti { get; set; }

        public bool IsDefault { get; set; }

        public string PermissionName { get; set; }
       
        public string GlobalSettingName { get; set; }

        public string GlobalSettingDefaultValue { get; set; }

        public int Order { get; set; }

        public virtual ICollection<DashBoardWidgetUserDTO> Users { get; set; }

        public DashBoardWidgetsDTO()
        {
            Users = new Collection<DashBoardWidgetUserDTO>();
        }
    }
}

using AutoMapper;

using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.DashBoard
{
   
    public class DashBoardWidgetUserDTO : AuditedEntityDto<Guid>, IAuditedObject, ITransientDependency
    {
        public Guid DashboardWidgetId { get; set; }

        public string UserSettings { get; set; }

    }
}

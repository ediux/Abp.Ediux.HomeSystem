using Ediux.HomeSystem.SystemManagement;

using System;

using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Commons.DTOs
{
    public class RatingsDto : CreationAuditedEntityWithUserDto<Guid, UserInforamtionDto>
    {
        public TenantDto Tenant { get; set; }

        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public short StarCount { get; set; }
    }
}

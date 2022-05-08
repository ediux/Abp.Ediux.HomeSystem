using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.SystemManagement;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Commons.DTOs
{
    public class TagDto : FullAuditedEntityWithUserDto<Guid, UserInforamtionDto>
    {
        [StringLength(32)]
        [MaxLength(32)]
        [Required]
        [DisplayName(HomeSystemResource.Common.Fields.TagName)]
        public string Name { get; set; }

        [StringLength(64)]
        [MaxLength(64)]
        [Required]
        [DisplayName(HomeSystemResource.Common.Fields.EntityType)]
        public string EntityType { get; set; }

        public TenantDto Tenant { get; set; }

        public virtual ICollection<TagEntityMapperDto> Entities { get; set; } = new HashSet<TagEntityMapperDto>();

    }
}

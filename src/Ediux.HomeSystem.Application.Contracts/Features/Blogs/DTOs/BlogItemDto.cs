using Ediux.HomeSystem.SystemManagement;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Blogs.DTOs
{
    public class BlogItemDto : ExtensibleFullAuditedEntityWithUserDto<Guid, UserInforamtionDto>
    {
        [MaxLength(64)]
        [StringLength(64)]
        [Required]
        public string Name { get; set; }

        [MaxLength(64)]
        [StringLength(64)]
        [Required]
        public string Slug { get; set; }

        public virtual TenantDto Tenant { get; set; }
    }
}

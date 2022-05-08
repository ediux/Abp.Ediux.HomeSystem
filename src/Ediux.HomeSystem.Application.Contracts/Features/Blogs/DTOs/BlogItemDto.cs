using Ediux.HomeSystem.SystemManagement;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Blogs.DTOs
{
    public class BlogItemDto : ExtensibleFullAuditedEntityWithUserDto<Guid, UserInforamtionDto>
    {
        [MaxLength(64)]
        [StringLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        [StringLength(64)]
        public string Slug { get; set; }

        public virtual TenantDto Tenant { get; set; }

        [JsonIgnore]
        public virtual ICollection<BlogPostItemDto> Posts { get; set; }
    }
}

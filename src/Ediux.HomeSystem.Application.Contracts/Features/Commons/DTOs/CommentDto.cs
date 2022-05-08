using Ediux.HomeSystem.Features.Blogs.DTOs;
using Ediux.HomeSystem.SystemManagement;

using System;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Commons.DTOs
{
    public class CommentDto : CreationAuditedEntityWithUserDto<Guid, UserInforamtionDto>
    {
        public virtual TenantDto Tenant { get; set; }

        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public string Text { get; set; }
        [JsonIgnore]
        public virtual CommentDto RepliedComment { get; set; }

        [JsonIgnore]
        public virtual BlogPostItemDto Post { get; set; }        
    }
}

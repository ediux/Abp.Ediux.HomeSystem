using System;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class Comments : CreationAuditedAggregateRoot<Guid>
    {
        public Guid? TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
        public string EntityType { get; set; }

        public  string EntityId { get; set; }

        public string Text { get; set; }

        public Guid? RepliedCommentId { get; set; }

        public virtual Comments RepliedComment { get; set; }
    }
}

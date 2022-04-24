using System;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class Ratings : CreationAuditedAggregateRoot<Guid>
    {
        public Guid? TenantId { get; set; }
        
        public virtual Tenant Tenant { get; set; }

        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public short StarCount { get; set; }
    }
}

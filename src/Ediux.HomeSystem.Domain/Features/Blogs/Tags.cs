using System;
using System.Collections.Generic;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class Tags : FullAuditedAggregateRoot<Guid>
    {
        public Guid? TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public string EntityType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EntityTags> Entities { get; set; }

        public Tags()
        {
            Entities = new HashSet<EntityTags>();
        }
    }
}

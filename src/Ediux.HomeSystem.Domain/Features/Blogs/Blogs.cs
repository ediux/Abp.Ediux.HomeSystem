using System;
using System.Collections.Generic;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class Blogs : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public Guid? TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual ICollection<BlogPosts> Posts { get; set; }
    }
}

using Ediux.HomeSystem.SystemManagement;

using System;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.CMS
{
    public class Pages : FullAuditedAggregateRoot<Guid>
    {
        public Guid? TenantId { get; set; }

        public virtual Tenant Tenant { get;set;}

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Content { get; set; }

        public string Script { get; set; }

        public string Style { get; set; }

        public MenuItems MenuItem { get; set; }
    }
}

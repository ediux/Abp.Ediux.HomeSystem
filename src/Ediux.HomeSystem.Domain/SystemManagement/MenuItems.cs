using Ediux.HomeSystem.Features.CMS;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.SystemManagement
{
    public class MenuItems : AuditedAggregateRoot<Guid>
    {
        public Guid? ParentId { get; set; }

        public string DisplayName { get; set; }

        public bool IsActive { get; set; }

        public string Url { get; set; }

        [MaxLength]
        public string Icon { get; set; }

        public int Order { get; set; }

        [MaxLength]
        public string Target { get; set; }
        [MaxLength]
        public string ElementId { get; set; }
        [MaxLength]
        public string CssClass { get; set; }

        public Guid? PageId { get; set; }

        public virtual Pages Page { get; set; }

        public Guid? TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual ICollection<MenuItems> SubMenuItems { get; set; }

        public MenuItems()
        {
            SubMenuItems = new HashSet<MenuItems>();
        }
    }
}

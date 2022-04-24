using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Volo.Abp.Domain.Entities;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class EntityTags : Entity
    {
      
        
        public Guid TagId { get; set; }

        public virtual Tags Tags { get; set; }

      
        
        public string EntityId { get; set; }

        public Guid? TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { TagId, EntityId };
        }
    }
}

using Ediux.HomeSystem.SystemManagement;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class BlogPosts : FullAuditedAggregateRoot<Guid>
    {
        public Guid BlogId { get; set; }

        public virtual Blogs Blog { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string ShortDescription { get; set; }

        [MaxLength]
        public string Content { get; set; }

        public Guid? CoverImageMediaId { get; set; }

        public virtual File_Store CoverImageMedia { get; set; }

        public Guid? TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }

        public Guid AuthorId { get; set; }

        public virtual IdentityUser Author { get; set; }


    }
}

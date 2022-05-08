using Ediux.HomeSystem.Features.Blogs.DTOs;
using Ediux.HomeSystem.SystemManagement;

using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Commons.DTOs
{
    public class TagEntityMapperDto : EntityDto
    {
        public string EntityId { get; set; }

        public TagDto Tag { get; set; }

        public TenantDto Tenant { get; set; }

        public FileStoreDto Photo { get; set; }

        public BlogPostItemDto Post { get; set; }

        public BlogItemDto Blog { get; set; }
    }
}

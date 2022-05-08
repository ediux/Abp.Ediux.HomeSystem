using Ediux.HomeSystem.Features.Commons.DTOs;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.SystemManagement;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Features.Blogs.DTOs
{
    public class BlogPostItemDto : FullAuditedEntityWithUserDto<Guid, UserInforamtionDto>
    {
        public virtual TenantDto Tenant { get; set; }

       
        [DisplayName(HomeSystemResource.Features.Blogs.Prefix)]
        public BlogItemDto Blog { get; set; } = new BlogItemDto();

        [StringLength(64)]
        [MaxLength(64)]
        [Required]
        [DisplayName(HomeSystemResource.Common.Fields.Title)]
        public string Title { get; set; }

        [MaxLength(256)]
        [StringLength(256)]
        [Required]
        [DisplayName(HomeSystemResource.Common.Fields.Slug)]
        public string Slug { get; set; }

        [MaxLength(256)]
        [StringLength(256)]
        [DisplayName(HomeSystemResource.Common.Fields.ShortDescription)]
        public string ShortDescription { get; set; }

        [MaxLength]
        [DisplayName(HomeSystemResource.Common.Fields.Context)]
        public string Context { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.CoverImageMedia)]
        public FileStoreDto CoverImageMedia { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.Author)]
        public UserInforamtionDto Author { get; set; }

        public virtual ICollection<CommentDto> Comments { get; set; }
    }
}

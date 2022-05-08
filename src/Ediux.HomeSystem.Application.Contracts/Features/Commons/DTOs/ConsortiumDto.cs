using Ediux.HomeSystem.SystemManagement;

using System;

using Volo.Abp.Application.Dtos;

namespace Ediux.HomeSystem.Features.Commons.DTOs
{
    public class ConsortiumDto : FullAuditedEntityWithUserDto<Guid, UserInforamtionDto>
    {
        public string Name { get; set; }

        public bool IsPrivateEnterprise { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string GroupRepresentative { get; set; }

        public string Description { get; set; }

        public virtual FileStoreDto LogoFile { get; set; }
    }
}

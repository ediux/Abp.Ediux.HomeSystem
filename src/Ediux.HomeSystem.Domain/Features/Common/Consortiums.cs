using Ediux.HomeSystem.SystemManagement;

using System;

using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Features.Common
{
    /// <summary>
    /// 表示財團/企業集團的資料表
    /// </summary>
    public class Consortiums : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public bool IsPrivateEnterprise { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string GroupRepresentative { get; set; }

        public string Description { get; set; }

        public Guid? LogoFileRef { get; set; }

        public virtual File_Store LogoFile { get; set; }
    }
}

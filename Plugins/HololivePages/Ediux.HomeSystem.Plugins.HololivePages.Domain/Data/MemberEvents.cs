using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Plugins.HololivePages.Data
{
    /// <summary>
    /// 成員事件簿
    /// </summary>
    public class MemberEvents : AuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public Guid MemberId { get; set; }

        public virtual Members Member { get; set; }

        public MemberEvents()
        {
            Member = new Members();
        }
    }
}

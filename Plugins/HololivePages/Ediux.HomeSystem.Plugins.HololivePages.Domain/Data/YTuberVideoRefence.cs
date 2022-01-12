using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Plugins.HololivePages.Data
{
    public class YTuberVideoRefence : AuditedEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Guid MemberId { get; set; }

        public virtual Members Member { get; set; }

        public YTuberVideoRefence()
        {
            Member = new Members();
        }
    }
}

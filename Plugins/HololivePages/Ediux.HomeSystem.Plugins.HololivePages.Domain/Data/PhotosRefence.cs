using System;
using Volo.Abp.Domain.Entities;

namespace Ediux.HomeSystem.Plugins.HololivePages.Data
{
    public class PhotosRefence : Entity
    {
        public Guid FileId { get; set; }
        public Guid MemberId { get; set; }

        public virtual Members Member { get; set; }

        public PhotosRefence()
        {
            Member = new Members();
        }

        public override object[] GetKeys()
        {
            return new object[] { FileId, MemberId };
        }
    }
}

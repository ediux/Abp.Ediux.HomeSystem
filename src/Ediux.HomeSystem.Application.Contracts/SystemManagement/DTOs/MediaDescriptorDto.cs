using System;

using Volo.Abp.Application.Dtos;

namespace Ediux.HomeSystem.SystemManagement
{
    [Serializable]
    public class MediaDescriptorDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string MimeType { get; set; }

        public int Size { get; set; }
    }
}

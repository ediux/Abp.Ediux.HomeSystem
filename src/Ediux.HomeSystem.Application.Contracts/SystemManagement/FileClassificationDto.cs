using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;

namespace Ediux.HomeSystem.SystemManagement
{
    [Serializable]
    public class FileClassificationDto : ExtensibleAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public FileClassificationDto Parent { get; set; }

        [JsonIgnore]
        public virtual ICollection<FileClassificationDto> Childs { get; set; }
    }
}

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

        [JsonIgnore]
        public FileClassificationDto Parent { get; set; }
        
        public virtual ICollection<FileClassificationDto> Childs { get; set; }
    }
}

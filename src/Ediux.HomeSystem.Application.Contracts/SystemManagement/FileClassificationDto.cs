using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    public class FileClassificationDto : ExtensibleAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public FileClassificationDto Parent { get; set; }

        public virtual ICollection<FileClassificationDto> Childs { get; set; }
    }
}

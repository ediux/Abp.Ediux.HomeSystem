using AutoMapper;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.Files
{
    public class FileStoreDTO : EntityDto<Guid>, ITransientDependency
    {
        public string Name { get; set; }
        public string ExtName { get; set; }
        public string Description { get; set; }
        public long Size { get; set; }
        public string OriginFullPath { get; set; }
        [IgnoreMap]
        public string Creator { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime CreatorDate { get; set; } = DateTime.Now;
        [IgnoreMap]
        public string Modifier { get; set; }
        public Guid? ModifierId { get; set; }
        public DateTime? ModifierDate { get; set; }
        public bool IsDeleted { get; set; }        
        public string ContentType { get; set; }
        [IgnoreMap]
        public byte[] FileContent { get; set; } = null;
        public int MIMETypeId { get; set; }
        [IgnoreMap]
        public bool IsAutoSaveFile { get; set; }
    }
}

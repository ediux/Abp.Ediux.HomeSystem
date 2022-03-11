using System;

using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    public class FileStoreDto : ExtensibleEntityDto<Guid>, ITransientDependency
    {
        public string Name { get; set; }
        public string ExtName { get; set; }
        public string Description { get; set; }
        public long Size { get; set; }
        public string OriginFullPath { get; set; }
       
        public string Creator { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime CreatorDate { get; set; } = DateTime.Now;
       
        public string Modifier { get; set; }
        public Guid? ModifierId { get; set; }
        public DateTime? ModifierDate { get; set; }
        public bool IsDeleted { get; set; }        
        public string ContentType { get; set; }
       
        public byte[] FileContent { get; set; } = null;
        public int MIMETypeId { get; set; }
        
        public bool IsAutoSaveFile { get; set; }
        //[IgnoreMap]
        //public bool IsCrypto { get; set; }
       
        public string SMBFullPath { get; set; }
        
        public string SMBLoginId { get; set; }
       
        public string SMBPassword { get; set; }
        
        public bool StorageInSMB { get; set; }
    }
}

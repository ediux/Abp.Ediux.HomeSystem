using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.ProductKeysBook
{
    public class ProductKeysBookDTO : EntityDto<Guid>, IFullAuditedObject, ITransientDependency
    {
        public string ProductName { get; set; }
        public string ProductKey { get; set; }
        public bool Shared { get; set; }
         

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

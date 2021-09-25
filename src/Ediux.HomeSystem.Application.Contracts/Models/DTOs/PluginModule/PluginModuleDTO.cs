
using AutoMapper;

using System;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.PluginModule
{

    public class PluginModuleDTO : EntityDto<Guid>, IFullAuditedObject, ITransientDependency
    {
        public string Name { get; set; }
        
        public string PluginPath { get; set; }

        /// <summary>
        /// Indicates whether the plugin module is disabled and will not be loaded.
        /// </summary>
        /// <value>bool</value>
        public virtual bool Disabled { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

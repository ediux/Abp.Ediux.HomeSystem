using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.PersonalCalendar
{
    public class PersonalCalendarItemDTO : AuditedEntityDto<Guid>, IAuditedObject, ITransientDependency
    {
        
        [JsonPropertyName("title")]
        [Required]
        [MaxLength(500)]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [JsonPropertyName("start")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("end")]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Required]
        [JsonPropertyName("allDay")]
        public bool AllDay { get; set; }
        
        [JsonPropertyName("url")]
        public string url { get; set; }

        [MaxLength]
        [JsonPropertyName("classNames")]
        public string classNames { get; set; }
        
        [Required]
        [JsonPropertyName("editable")]
        public bool editable { get; set; }
        
        [Required]
        [JsonPropertyName("startEditable")]
        public bool startEditable { get; set; }
        
        [Required]
        [JsonPropertyName("durationEditable")]
        public bool durationEditable { get; set; }
        
        [Required]
        [JsonPropertyName("resourceEditable")]
        public bool resourceEditable { get; set; }
        
        [MaxLength(50)]
        [JsonPropertyName("icon")]
        public string icon { get; set; }
        
        [MaxLength]
        [JsonPropertyName("description")]
        public string description { get; set; }
    }
}

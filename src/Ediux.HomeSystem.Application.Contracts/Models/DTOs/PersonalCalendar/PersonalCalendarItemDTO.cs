using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public PersonalCalendarItemDTO()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now.AddDays(1);
        }

        [Required]
        [MaxLength(500)]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Required]
        public bool AllDay { get; set; }

        public string url { get; set; }
        
        [Required]
        public bool editable { get; set; }

        [Required]
        public bool startEditable { get; set; }

        [Required]
        public bool durationEditable { get; set; }

        [Required]
        public bool resourceEditable { get; set; }

        [MaxLength(50)]
        public string icon { get; set; }

        [MaxLength]
        public string description { get; set; }

       
    }
}

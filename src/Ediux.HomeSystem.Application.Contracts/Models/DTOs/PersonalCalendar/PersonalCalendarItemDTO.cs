﻿using System;
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

    public class PersonalCalendarItemDTO : ExtensibleAuditedEntityDto<Guid>, ITransientDependency
    {
        public PersonalCalendarItemDTO()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now.AddDays(1);
        }

        public PersonalCalendarItemDTO(Guid Id) : this()
        {
            this.Id = Id;
        }

        [Required]
        [MaxLength(500)]
        [StringLength(500)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [JsonPropertyName("start")]
        public DateTime? StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [JsonPropertyName("end")]
        public DateTime? EndTime { get; set; }

        [Required]
        [JsonPropertyName("allDay")]
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

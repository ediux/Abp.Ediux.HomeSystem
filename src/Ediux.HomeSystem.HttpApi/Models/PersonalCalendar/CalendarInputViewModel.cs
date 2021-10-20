using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.PersonalCalendar
{
    public class CalendarInputViewModel : ITransientDependency
    {
        public CalendarInputViewModel()
        {
            //Volo.Abp.Identity.IIdentityUserAppService
            StartTime = DateTime.Now;
            EndTime = DateTime.Now.AddDays(1);
            editable = true;
            startEditable = true;
            durationEditable = true;
            resourceEditable = false;
        }

        [Required]
        [DisplayName("Field:Id")]
        [HiddenInput]
        [JsonPropertyName("id")]
        public virtual Guid Id { get; set; }

        [JsonPropertyName("groupId")]
        [DisplayName("Field:GroupId")]
        [HiddenInput]
        [MaxLength(256)]
        public virtual string GroupId { get; set; }

        [Required]
        [JsonPropertyName("allDay")]
        [DisplayName("Field:AllDay")]
        public virtual bool AllDay { get; set; }

        [Required]
        [JsonPropertyName("start")]
        [DataType(DataType.DateTime)]
        [DisplayName("Field:StartTime")]
        public virtual DateTime StartTime { get; set; }
        
        [JsonPropertyName("end")]
        [DataType(DataType.DateTime)]
        [DisplayName("Field:EndTime")]
        public virtual DateTime? EndTime { get; set; }

        [Required]
        [MaxLength(500)]
        [StringLength(500)]
        [JsonPropertyName("title")]
        [DisplayName("Field:Calendar_Event_Title")]      
        public virtual string Title { get; set; }

        [JsonPropertyName("url")]
        [DisplayName("Field:Url")]
        [HiddenInput]
        [MaxLength(2048)]
        public string url { get; set; }

        [MaxLength(2048)]
        [JsonPropertyName("classNames")]
        [HiddenInput]
        [DisplayName("Field:StyleSheet")]
        public virtual string classNames { get; set; }

        [Required]
        [JsonPropertyName("editable")]
        [DisplayName("Field:Editable")]
        [HiddenInput]
        public virtual bool editable { get; set; }

        [Required]
        [HiddenInput]
        [JsonPropertyName("startEditable")]
        [DisplayName("Field:StartEditable")]
        public virtual bool startEditable { get; set; }

        [Required]
        [HiddenInput]
        [JsonPropertyName("durationEditable")]
        [DisplayName("Field:DurationEditable")]
        public virtual bool durationEditable { get; set; }

        [Required]
        [HiddenInput]
        [JsonPropertyName("resourceEditable")]
        [DisplayName("Field:ResourceEditable")]
        public virtual bool resourceEditable { get; set; }

        [MaxLength(50)]
        [JsonPropertyName("icon")]
        [DisplayName("Field:Calendar_Event_Icon")]
        public virtual string icon { get; set; }

        [MaxLength(2048)]
        [JsonPropertyName("description")]
        [DisplayName("Field:Description")]        
        public virtual string description { get; set; }
    }
}

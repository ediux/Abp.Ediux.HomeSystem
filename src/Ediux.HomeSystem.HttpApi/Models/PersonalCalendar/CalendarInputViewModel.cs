using Microsoft.AspNetCore.Mvc;

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
        }

        [Required]
        [DisplayName("Field:Id")]
        [HiddenInput]
        [Display(AutoGenerateField = false)]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("groupId")]
        [DisplayName("Field:GroupId")]
        [HiddenInput]
        [MaxLength(256)]
        public string GroupId { get; set; }

        [Required]
        [JsonPropertyName("allDay")]
        [DisplayName("Field:AllDay")]
        public bool AllDay { get; set; }

        [Required]
        [JsonPropertyName("start")]
        [DataType(DataType.DateTime)]
        [DisplayName("Field:StartTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("end")]
        [DataType(DataType.DateTime)]
        [DisplayName("Field:EndTime")]
        public DateTime? EndTime { get; set; }

        [Required]
        [MaxLength(500)]
        [StringLength(500)]
        [JsonPropertyName("title")]
        [DisplayName("Field:Calendar_Event_Title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        [DisplayName("Field:Url")]
        [MaxLength]
        public string url { get; set; }

        [MaxLength(2048)]
        [JsonPropertyName("classNames")]
        [Display(AutoGenerateField = false)]
        [DisplayName("Field:StyleSheet")]        
        public string classNames { get; set; }

        [Required]
        [JsonPropertyName("editable")]
        [DisplayName("Field:Editable")]
        public bool editable { get; set; }

        [Required]
        [JsonPropertyName("startEditable")]
        [DisplayName("Field:StartEditable")]
        public bool startEditable { get; set; }

        [Required]
        [JsonPropertyName("durationEditable")]
        [DisplayName("Field:DurationEditable")]
        public bool durationEditable { get; set; }

        [Required]
        [JsonPropertyName("resourceEditable")]
        [DisplayName("Field:ResourceEditable")]
        public bool resourceEditable { get; set; }

        [MaxLength(50)]
        [JsonPropertyName("icon")]
        [DisplayName("Field:Calendar_Event_Icon")]
        public string icon { get; set; }

        [MaxLength(2048)]
        [JsonPropertyName("description")]
        [DisplayName("Field:Description")]
        public string description { get; set; }
    }
}

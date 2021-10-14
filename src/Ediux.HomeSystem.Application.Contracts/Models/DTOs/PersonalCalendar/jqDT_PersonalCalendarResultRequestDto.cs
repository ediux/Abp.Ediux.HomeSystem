using Ediux.HomeSystem.Models.DTOs.jqDataTables;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;

namespace Ediux.HomeSystem.Models.DTOs.PersonalCalendar
{
    public class jqDT_PersonalCalendarResultRequestDto : PagedAndSortedResultRequestDto
    {
        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }
    }
}

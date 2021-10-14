using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.PersonalCalendar
{
    public class PersonalCalendarRequestDTO : ITransientDependency
    {
        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("lazyFetching")]
        public bool LazyFetching { get; set; }
    }
         
}

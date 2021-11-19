using System;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.PersonalCalendar
{
    public class PersonalCalendarRequestDTO : PagedAndSortedResultRequestDto, ITransientDependency
    {
        public PersonalCalendarRequestDTO()
        {

        }

        public PersonalCalendarRequestDTO(string startTime, string endTime)
        {
            if (!string.IsNullOrWhiteSpace(startTime))
            {
                Start = DateTime.Parse(startTime);
            }

            if (!string.IsNullOrWhiteSpace(endTime))
            {
                End = DateTime.Parse(endTime);
            }
        }

        [JsonPropertyName("start")]
        public DateTime? Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime? End { get; set; }
    }

}

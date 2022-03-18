using Ediux.HomeSystem.SystemManagement;

using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    /// <summary>
    /// 個人行事曆資料實體轉換物件
    /// </summary>
    public class PersonalCalendarDto : ExtensibleEntityDto<Guid>
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Color { get; set; }
        /// <summary>
        /// 是否為整天?
        /// </summary>
        public bool IsAllDay { get; set; }

        public PersonalCalendarDto Copy() => MemberwiseClone() as PersonalCalendarDto;

        public string Description { get; set; } 

        public string UIAction { get; set; }
    }
}

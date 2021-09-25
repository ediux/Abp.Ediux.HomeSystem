using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Data
{
    /// <summary>
    /// 個人行事曆
    /// </summary>
    public class PersonalCalendar: FullAuditedAggregateRoot<Guid>, IFullAuditedObject
    {
       
        //[Required]
        //[MaxLength(256)]
        public string EventId { get; set; }

        //[MaxLength(256)]
        public string groupId { get; set; }
        //[Required]
        public bool allDay { get; set; }
        //[MaxLength(20)]
        public string t_start { get; set; }
        //[MaxLength(20)]
        public string t_end { get; set; }
        //[Required]
        //[MaxLength(500)]
        public string title { get; set; }
        //[MaxLength]
        public string url { get; set; }
        //[MaxLength]
        public string classNames { get; set; }
        //[Required]
        public bool editable { get; set; }
        //[Required]
        public bool startEditable { get; set; }
        //[Required]
        public bool durationEditable { get; set; }
        //[Required]
        public bool resourceEditable { get; set; }
        //[MaxLength(450)]
        //[Required]
        //[Required]
        public bool IsAdded { get; set; }
        //[MaxLength(50)]
        public string icon { get; set; }
        //[MaxLength]
        public string description { get; set; }
    }
}

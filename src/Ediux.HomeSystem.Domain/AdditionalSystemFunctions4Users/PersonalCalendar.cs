using Ediux.HomeSystem.SystemManagement;

using System;

using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    /// <summary>
    /// 個人行事曆
    /// </summary>
    public class PersonalCalendar : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 參考的事件識別碼(多天行事曆事件使用)
        /// </summary>
        public Guid? RefenceEventId { get; set; }

        /// <summary>
        /// 參考的事件
        /// </summary>
        public PersonalCalendar RefenceEvent { get; set; }

        /// <summary>
        /// 是否為整天?
        /// </summary>
        public bool IsAllDay { get; set; }

        /// <summary>
        /// 起始時間
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 行事曆事件訊息內容識別碼
        /// </summary>
        public Guid SystemMessageId { get; set; }

        /// <summary>
        /// 行事曆事件訊息內容
        /// </summary>
        public InternalSystemMessages SystemMessages { get; set; }

        public string Color { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.SystemManagement
{
    /// <summary>
    /// 儲存內部訊息傳遞(包含要推送到用戶端裝置的訊息)
    /// </summary>
    public class InternalSystemMessages : FullAuditedAggregateRoot<Guid>, IFullAuditedObject
    {
        /// <summary>
        /// 發送者使用者識別碼
        /// </summary>
        public Guid FromUserId { get; set; }

        /// <summary>
        /// 發送者
        /// </summary>
        public virtual IdentityUser From { get; set; }

        /// <summary>
        /// 主旨
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 訊息內容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否為電子郵件?
        /// </summary>
        public bool IsEMail { get; set; }

        /// <summary>
        /// 是否為推送訊息
        /// </summary>
        public bool IsPush { get; set; }
        
        /// <summary>
        /// 是否為副本?(IsEMail為True才作用)
        /// </summary>
        public bool? IsCC { get; set; }

        /// <summary>
        /// 是否為密件副本?(IsEMail為True才作用)
        /// </summary>
        public bool? IsBCC { get; set; }

        /// <summary>
        /// 發送時間
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 收件/讀取時間
        /// </summary>
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 是否已讀?
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 推送點擊動作回呼URL
        /// </summary>
        public string ActionCallbackURL { get; set; }

        /// <summary>
        /// 附加檔案
        /// </summary>
        public virtual ICollection<AttachFile> AttachFiles { get; set; }

        /// <summary>
        /// 參照的訊息識別碼
        /// </summary>
        public Guid? RefenceMessageId { get; set; }

        /// <summary>
        /// 參照的訊息
        /// </summary>
        public virtual InternalSystemMessages RefenceMessage { get; set; }

        /// <summary>
        /// 是否為回覆訊息?
        /// </summary>
        public bool IsReply { get; set; }

        public InternalSystemMessages()
        {
            AttachFiles = new HashSet<AttachFile>();
        }
    }
}

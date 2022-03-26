using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;

namespace Ediux.HomeSystem.SystemManagement
{
    public class SystemMessageDto : ExtensibleFullAuditedEntityWithUserDto<Guid, UserInforamtionDto>
    {
        /// <summary>
        /// 發送者
        /// </summary>
        public virtual UserInforamtionDto From { get; set; }

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
        /// 是否已讀?
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 發送時間
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 收件/讀取時間
        /// </summary>
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 推送點擊動作回呼URL
        /// </summary>
        public string ActionCallbackURL { get; set; }

        /// <summary>
        /// 附加檔案
        /// </summary>
        public virtual ICollection<FileStoreDto> AttachFiles { get; set; }

       

        /// <summary>
        /// 參照的訊息
        /// </summary>
        public virtual SystemMessageDto RefenceMessage { get; set; }

        /// <summary>
        /// 是否為回覆訊息?
        /// </summary>
        public bool IsReply { get; set; }
    }
}


using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public class UserPasswordStore : AuditedAggregateRoot<long>, IAuditedObject
    {
        /// <summary>
        /// 網站URL
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// 網站名稱
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 是否為歷史紀錄?
        /// </summary>
        public bool IsHistory { get; set; }

    }
}

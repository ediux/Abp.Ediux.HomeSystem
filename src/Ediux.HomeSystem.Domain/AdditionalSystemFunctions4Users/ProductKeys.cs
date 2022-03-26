using System;

using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public class ProductKeys : AuditedAggregateRoot<Guid>
    {

        /// <summary>
        /// 產品名稱
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 產品金鑰
        /// </summary>
        public string ProductKey { get; set; }
        /// <summary>
        /// 是否公開?
        /// </summary>
        public bool Shared { get; set; }


    }
}

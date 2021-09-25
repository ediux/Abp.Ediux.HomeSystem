using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Data
{
    public class AbpPlugins : FullAuditedAggregateRoot<Guid>, IFullAuditedObject
    {
        /// <summary>
        /// 擴充模組登記名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 組件路徑
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 啟用/停用
        /// </summary>
        public bool Disabled { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}

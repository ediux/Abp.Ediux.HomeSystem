using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions
{
    public class HoloDepartmentsDto : AuditedEntityDto<Guid>, ITransientDependency
    {
        /// <summary>
        /// 組別名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 組別簡述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 開始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 結束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        public virtual ICollection<HoloMemberDto> Members { get; set; }

        public HoloDepartmentsDto()
        {
            Members = new List<HoloMemberDto>();
        }
    }
}

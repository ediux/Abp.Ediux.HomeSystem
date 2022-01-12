using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions
{
    public class HoloBranchesDto : AuditedEntityDto<Guid>, ITransientDependency
    {
        /// <summary>
        /// 品牌名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 品牌創立時間
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 品牌結束時間
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 品牌簡述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 品牌商標圖檔參考ID
        /// </summary>
        public Guid? LogoFileRef { get; set; }

        /// <summary>
        /// 集團商標檔案下載URL
        /// </summary>      
        public string LogoFileURL
        {
            get
            {
                return LogoFileRef.HasValue ? $"/api/simpleupload/download/{LogoFileRef}" : string.Empty;
            }
        }

        public string Creator { get; set; }

        public string Modifier { get; set; }

       public virtual ICollection<HoloDepartmentsDto> Departments { get; set; }

        public HoloBranchesDto()
        {
            Departments = new List<HoloDepartmentsDto>();
        }
    }
}

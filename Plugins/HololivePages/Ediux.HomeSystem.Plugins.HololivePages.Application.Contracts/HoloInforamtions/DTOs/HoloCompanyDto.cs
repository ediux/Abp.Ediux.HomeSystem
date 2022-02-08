using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions
{
    public class HoloCompanyDto : AuditedEntityDto<Guid>, ITransientDependency
    {
        /// <summary>
        /// 公司名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否為總公司
        /// </summary>
        public bool IsHQ { get; set; }

        /// <summary>
        /// 負責地區
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 註冊日期
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// 開始營運日期
        /// </summary>
        public DateTime? StartOperationDate { get; set; }

        /// <summary>
        /// 結束營運日期
        /// </summary>
        public DateTime? EndOperationDate { get; set; }

        /// <summary>
        /// 公司負責人
        /// </summary>
        public string CompanyRepresentative { get; set; }

        /// <summary>
        /// 公司執行長姓名
        /// </summary>
        public string CEO { get; set; }

        /// <summary>
        /// 公司簡述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 公司商標圖檔參考ID
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

        public virtual ICollection<HoloBranchesDto> Branches { get; set; }

        public HoloCompanyDto()
        {
            Branches = new List<HoloBranchesDto>();
        }
    }
}

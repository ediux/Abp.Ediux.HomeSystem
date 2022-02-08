using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions
{
    public class HoloGroupDTO : AuditedEntityDto<Guid>, ITransientDependency
    {
        /// <summary>
        /// 集團名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 註冊日期
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// 集團負責人
        /// </summary>
        public string GroupRepresentative { get; set; }

        /// <summary>
        /// 集團簡述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 集團商標圖檔參考ID
        /// </summary>
        public Guid? LogoFileRef { get; set; }

        /// <summary>
        /// 集團商標檔案下載URL
        /// </summary>      
        public string LogoFileURL { get {
                return LogoFileRef.HasValue ? $"/api/simpleupload/download/{LogoFileRef}" : string.Empty;
            } }

        public virtual ICollection<HoloCompanyDto> Companies { get; set; }

        public string Creator { get; set; }

        public string Modifier { get; set; }

        public HoloGroupDTO()
        {
            Companies = new HashSet<HoloCompanyDto>();
        }
    }
}

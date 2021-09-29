using System.ComponentModel.DataAnnotations;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.PassworkBook
{
    public class PassworkBookDTO : AuditedEntityDto<long>, IAuditedObject, ITransientDependency
    {
        
        [Display(Name = "網站名稱", Order = 1)]
        public string SiteName { get; set; }

        [Display(Name = "登入帳號", Order = 2)]
        public string LoginAccount { get; set; }

        [Display(Name = "登入密碼", Order = 3)]
        public string Password { get; set; }

        [Display(Name = "網站URL", Order = 4)]
        public string SiteURL { get; set; }

        [Display(Name = "歷史紀錄", Order = 5)]
        public bool IsHistory { get; set; }
    }
}

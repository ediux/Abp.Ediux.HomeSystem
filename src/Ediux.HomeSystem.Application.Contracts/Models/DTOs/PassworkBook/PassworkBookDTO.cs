using Ediux.HomeSystem.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.PassworkBook
{
    public class PassworkBookDTO : ExtensibleAuditedEntityDto<long>, ITransientDependency
    {

        [DisplayName(HomeSystemResource.Common.Fields.SiteName)]
        public string SiteName { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.LoginAccount)]
        public string LoginAccount { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.Password)]
        public string Password { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.SiteURL)]
        public string SiteURL { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.IsHistory)]
        public bool IsHistory { get; set; }

        public void SetDynamicInfo(string name,string value)
        {
            this.SetProperty(name, value);  
        }

        public string GetDynamicInfo(string name)
        {
            return this.GetProperty<string>(name);
        }

        public ListResultDto<string> ListAllDynamicName()
        {
            return new ListResultDto<string>(ExtraProperties.Keys.ToList());
        }
    }
}

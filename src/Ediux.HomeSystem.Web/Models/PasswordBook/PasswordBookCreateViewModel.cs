using Ediux.HomeSystem.Localization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;
using AutoMapper;
using Ediux.HomeSystem.Models.DTOs.PassworkBook;

namespace Ediux.HomeSystem.Web.Models.PasswordBook
{
    [AutoMap(typeof(PassworkBookDTO),ReverseMap =true)]
    public class PasswordBookCreateViewModel : ExtensibleObject
    {
        [DisplayName(HomeSystemResource.Common.Fields.SiteName)]
        public string SiteName { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.LoginAccount)]
        public string LoginAccount { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.Password)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.SiteURL)]
        public string SiteURL { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.IsHistory)]
        [HiddenInput]
        public bool IsHistory { get; set; }

        public void SetDynamicInfo(string name, string value)
        {
            this.SetProperty(name, value);
        }

        public string GetDynamicInfo(string name)
        {
            return this.GetProperty<string>(name);
        }
    }
}

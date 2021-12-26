using AutoMapper;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.MIMETypes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Ediux.HomeSystem.Web.Models.MIMETypeManager
{
    [AutoMap(typeof(MIMETypesDTO), ReverseMap = true)]
    public class MIMETypeManagerCreateModel
    {
        [Required]
        [MaxLength(256)]
        [StringLength(256)]
        [DisplayName(HomeSystemResource.Features.MIMETypes.DTFX.Columns.MIME)]
        public string MIME { get; set; }

        [Required]
        [MaxLength(256)]
        [StringLength(256)]
        [DisplayName(HomeSystemResource.Features.MIMETypes.DTFX.Columns.RefenceExtName)]
        public string RefenceExtName { get; set; }

        [DisplayName(HomeSystemResource.Features.MIMETypes.DTFX.Columns.Description)]
        [TextArea]
        public string Description { get; set; }
    }
}

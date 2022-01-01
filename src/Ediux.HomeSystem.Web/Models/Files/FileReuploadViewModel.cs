using AutoMapper;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Ediux.HomeSystem.Web.Models.Files
{
    [AutoMap(typeof(FileStoreDTO), ReverseMap = true)]
    public class FileReuploadViewModel
    {
        [Key]
        [Required]
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [DisplayName(HomeSystemResource.Features.Files.DTFX.Columns.Name)]
        public string Name { get; set; }

        [Required]
        [DisplayName(HomeSystemResource.Features.Files.DTFX.Columns.ExtName)]
        public string ExtName { get; set; }

        [ReadOnly(true)]
        public long Size { get; set; }

        [StringLength(2048)]
        [MaxLength(2048)]
        [TextArea(Rows = 3)]
        [DisplayName(HomeSystemResource.Features.Files.DTFX.Columns.Description)]
        public string Description { get; set; }
        [JsonIgnore]
        [IgnoreMap]
        [DisplayName(HomeSystemResource.Features.Files.DTFX.Columns.UploadFiles)]
        public IFormFile UploadFile { get; set; }

        [HiddenInput]
        [DisplayName(HomeSystemResource.Features.Files.IsAutoSaveFile)]
        public bool IsAutoSaveFile { get; set; }
    }
}

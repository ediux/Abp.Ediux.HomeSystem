using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.MIMETypes
{
    public class MIMETypesDTO : AuditedEntityDto<int>, IAuditedObject, ITransientDependency
    {
        [Required]
        [MaxLength(256)]
        [JsonPropertyName("mime")]
        public string MIME { get; set; }

        /// <summary>
        /// 對應的附檔名
        /// </summary>
        [MaxLength(256)]
        [JsonPropertyName("refenceExtName")]
        public string RefenceExtName { get; set; }

        /// <summary>
        /// 說明描述
        /// </summary>
        [MaxLength]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}

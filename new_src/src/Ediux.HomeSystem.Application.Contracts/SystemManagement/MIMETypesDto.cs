using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    public class MIMETypesDto : AuditedEntityDto<int>, ITransientDependency
    {
        [Required]
        [MaxLength(256)]
        [JsonPropertyName("mime")]
        public string ContentType { get; set; }

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

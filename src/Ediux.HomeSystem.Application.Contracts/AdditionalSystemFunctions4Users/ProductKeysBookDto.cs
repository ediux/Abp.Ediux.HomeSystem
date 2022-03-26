using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{

    public class ProductKeysBookDto : ExtensibleAuditedEntityDto<Guid>, ITransientDependency
    {
        /// <summary>
        /// 產品名稱
        /// </summary>
        [JsonPropertyName("productname")]
        public string ProductName { get; set; }
        /// <summary>
        /// 產品金鑰
        /// </summary>
        [JsonPropertyName("productkey")]
        public string ProductKey { get; set; }
        /// <summary>
        /// 公開/私用 旗標
        /// </summary>
        [JsonPropertyName("shared")]
        public bool Shared { get; set; }

        [JsonPropertyName("extraInfo")]
        public string ExtraInformation { get; set; }

        public ProductKeysBookDto()
        {
            Shared = false;            
            ExtraInformation = string.Empty;
        }


    }
}

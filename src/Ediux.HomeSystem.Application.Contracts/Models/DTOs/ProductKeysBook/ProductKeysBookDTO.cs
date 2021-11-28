using System;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;
using System.Text.Json.Serialization;
using AutoMapper;

namespace Ediux.HomeSystem.Models.DTOs.ProductKeysBook
{
    
    public class ProductKeysBookDTO : ExtensibleAuditedEntityDto<Guid>, ITransientDependency
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

        public ProductKeysBookDTO()
        {
            Shared = false;            
        }


    }
}

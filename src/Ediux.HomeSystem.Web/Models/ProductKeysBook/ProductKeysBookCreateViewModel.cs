using AutoMapper;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace Ediux.HomeSystem.Web.Models.ProductKeysBook
{
    public class ProductKeysBookCreateViewModel : ExtensibleObject
    {
        /// <summary>
        /// 產品名稱
        /// </summary>
        [DisplayName(HomeSystemResource.Features.ProductKeysBook.DTFX.Columns.ProductName)]
        [MaxLength(256)]
        [StringLength(256)]
        public string ProductName { get; set; }
        /// <summary>
        /// 產品金鑰
        /// </summary>
        [MaxLength(256)]
        [StringLength(256)]
        [DisplayName(HomeSystemResource.Features.ProductKeysBook.DTFX.Columns.ProductKey)]
        public string ProductKey { get; set; }

        /// <summary>
        /// 公開/私用 旗標
        /// </summary>
        [DisplayName(HomeSystemResource.Features.ProductKeysBook.DTFX.Columns.Flag_Shared)]
        [Required]
        public string Shared { get; set; }

        //[DisplayName(HomeSystemResource.Features.ProductKeysBook.DTFX.Columns.ExtendProperies)]
        //public ExtraPropertyDictionary ExtraProperties => extraPropertyDictionary;

        public ProductKeysBookCreateViewModel() : base()
        {

        }
    }
}

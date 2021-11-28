using System;
using Ediux.HomeSystem.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Models.ProductKeysBook
{
    public class ProductKeysBookUpdateViewModel : ProductKeysBookCreateViewModel
    {
        [DisplayName(HomeSystemResource.Common.Fields.Id)]
        [HiddenInput]
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}

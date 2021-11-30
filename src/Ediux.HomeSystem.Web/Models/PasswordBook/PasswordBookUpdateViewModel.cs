using AutoMapper;
using Ediux.HomeSystem.Models.DTOs.PassworkBook;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ediux.HomeSystem.Web.Models.PasswordBook
{
    [AutoMap(typeof(PassworkBookDTO),ReverseMap =true)]
    public class PasswordBookUpdateViewModel: PasswordBookCreateViewModel
    {
        [Key]
        [HiddenInput]
        public long Id { get; set; }
    }
}

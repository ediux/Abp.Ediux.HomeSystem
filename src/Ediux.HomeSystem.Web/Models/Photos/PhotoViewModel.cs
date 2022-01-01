using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ediux.HomeSystem.Web.Models.Photos
{
    public class PhotoViewModel
    {
        [Key]
        [Required]
        [HiddenInput]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        [IgnoreMap]
        public string Creator { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime CreatorDate { get; set; } = DateTime.Now;
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ediux.HomeSystem.Web.Models.MIMETypeManager
{
    public class MIMETypeManagerUpdateModel : MIMETypeManagerCreateModel
    {
        [Required]
        [HiddenInput]
        public int Id { get; set; }
    }
}

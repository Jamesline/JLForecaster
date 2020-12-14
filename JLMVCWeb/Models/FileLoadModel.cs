using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace JLForecasterWeb.Models
{
    public class FileLoadModel
    {
        [Required]
        [Display(Name = "File to be loaded")]
        public IFormFile FileContentLoaded { get; set; }
        [Required]
        [Display(Name = "File type")]
        public string FileTypeExt { get; set; }
        
    }
}

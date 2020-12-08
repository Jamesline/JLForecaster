using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JLForecasterWeb.Models
{
    public class FileService
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FullFilePath { get; set; }
        public string FileSize { get; set; }
        public IFormFile FileContent { get; set; }

    }
}

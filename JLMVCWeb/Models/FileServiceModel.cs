using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JLForecasterWeb.Models
{
    public class FileServiceModel
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fullFilePath { get; set; }
        public string fileSize { get; set; }
        public string fileExt { get; set; }
        public string status { get; set; }
        public IFormFile fileContent { get; set; }
        

    }
}

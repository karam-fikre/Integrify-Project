using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBotRangerCore.Models.ImagesModels
{
    public class UploadImageModel
    {
        public string  Title { get; set; }
        public IFormFile ImageFile { get; set; }
        
    }
}

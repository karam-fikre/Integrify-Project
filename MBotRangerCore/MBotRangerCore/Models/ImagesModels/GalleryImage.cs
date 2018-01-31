using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MBotRangerCore.Models.ImagesModels
{
    public class GalleryImage
    {
        [Key]
        public int Id { get; set; }
        public string  Title { get; set; }
        public DateTime Created { get; set; }
        public string  Url { get; set; }

    }
}

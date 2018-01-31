using CloudinaryDotNet;
using System.Collections.Generic;

namespace MBotRangerCore.Models.ImagesModels
{
    public class DictionaryModel : Model
    {
        public DictionaryModel(Cloudinary cloudinary, Dictionary<string, string> dict)
            : base(cloudinary)
        {
            Dict = dict;
        }

        public Dictionary<string, string> Dict { get; set; }
    }

  

    public class Model
    {
        public Model(Cloudinary cloudinary)
        {
            Cloudinary = cloudinary;
        }

        public Cloudinary Cloudinary { get; set; }
    }
}
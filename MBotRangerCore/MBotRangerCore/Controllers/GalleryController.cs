using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MBotRangerCore.Models;
using MBotRangerCore.Models.ImagesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;

namespace MBotRangerCore.Controllers
{
    public class GalleryController : Controller
    {
        private IConfiguration _config;
        private string ConnectionString { get; }
        private readonly MBotRangerCoreContext _context;
        private Account account = new Account(
                                            "dlxazvufc",
                                            "152192368129483",
                                            "gOgstsNVkTW8-lSAj3KptLwmgNM");

        public GalleryController(MBotRangerCoreContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
            ConnectionString = _config["ConnectionString"];
        }

        public IActionResult ImagesView()
        {
            var imagelist = _context.GalleryImage.AsParallel();
           
            var model = new GalleryIndexModel
            {
                Images = imagelist
            
            };
            return View(model);
        }



        [HttpGet]
        public IActionResult Upload()
        {

            var model = new UploadImageModel();
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file,string title)
        {
            Cloudinary cloudinary = new Cloudinary(account);
            var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            var image = new GalleryImage
            {
                Title = title,
                Url = uploadResult.Uri.AbsoluteUri,
                Created = DateTime.Now
            };
            _context.Add(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("ImagesView");
        }

    

   

    }
}
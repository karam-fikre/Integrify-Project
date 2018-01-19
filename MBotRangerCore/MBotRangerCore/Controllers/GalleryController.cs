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
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;

namespace MBotRangerCore.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHostingEnvironment _appEnv;
        private IConfiguration _config;
        private string ConnectionString { get; }
        private readonly MBotRangerCoreContext _context;
        private Account account = new Account(
                                            "dlxazvufc",
                                            "152192368129483",
                                            "gOgstsNVkTW8-lSAj3KptLwmgNM");

        public GalleryController(IHostingEnvironment appEnv, MBotRangerCoreContext context,IConfiguration config)
        {
            _appEnv = appEnv;
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




        [HttpPost]
        public ActionResult SaveSnapshot()
        {
            bool saved = false;
            Cloudinary cloudinary = new Cloudinary(account);
            string image = Request.Form["datatype"].ToString();
            var base64Data = Regex.Match(image, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);
            // var data = Convert.FromBase64String(image);
             var path = Path.GetTempFileName();
            // var path = Path.Combine(, "snapshot.png");
            var uploads = Path.Combine(_appEnv.WebRootPath, path);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(uploads)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            System.IO.File.WriteAllBytes(uploads, binData);
                saved = true;
            

            return Json(saved ? "image saved" : "image not saved");
        }

        public string EncodeBase64(string data)
        {
            string s = data.Trim().Replace(" ", "+");
            if (s.Length % 4 > 0)
                s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }
    }
}
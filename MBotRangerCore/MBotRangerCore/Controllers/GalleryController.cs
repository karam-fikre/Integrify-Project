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
        public Account account = new Account(
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
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file,string title)
        {
            Cloudinary cloudinary = new Cloudinary(account);



            // var container = GetBlobContainer(ConnectionString, "images");
            var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = content.FileName.Trim('"');


            // //Get a reference to Blcok Blob

            // var blockBlob = container.GetBlockBlobReference(fileName);
            //await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            //var headers = HttpContext.Request.Headers;
            //string contents = null;
            //using (StreamReader reader = new StreamReader(HttpContext.Request.Path))
            //{
            //    contents = reader.ReadToEnd();
            //}
            //Dictionary<string, string> results = new Dictionary<string, string>();
            //string[] pairs = contents.Split(new char[] { '&' },
            //  StringSplitOptions.RemoveEmptyEntries);

            //foreach (var pair in pairs)
            //{
            //    string[] splittedPair = pair.Split('=');
            //    results.Add(splittedPair[0], splittedPair[1]);
            //}


            var uploadParams = new ImageUploadParams()
            {
               
                File = new FileDescription(@"")
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
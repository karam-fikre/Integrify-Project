using MBotRangerCore.Models;
using MBotRangerCore.Models.ImagesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MBotRangerCore.Controllers
{
    public class GalleryController : Controller
    {
        MbotAppData galleryAppData;
        private readonly MBotRangerCoreContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
       

        public GalleryController(MBotRangerCoreContext context, UserManager<ApplicationUser> userManager)
        {
            galleryAppData = new MbotAppData();
            _context = context;
            _userManager = userManager;
          
        }





        //GET: get the Snapshots for all users
        public IActionResult ImagesView()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }
            ViewBag.WaitList = galleryAppData.users;
            var imagelist = _context.GalleryImage.AsParallel();
           
            var model = new GalleryIndexModel
            {
                Images = imagelist
            
            };
            return View(model);
        }





        //POST: Save the snapshot taken by the user
        [HttpPost]
        public async Task<IActionResult> SaveSnapshot()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }
            bool saved = false;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string image = Request.Form["datatype"].ToString();
            
            var databaseimage = new GalleryImage
            {
                Title = user.FirstName,
                Url = image.ToString(),
                Created = DateTime.Now
            };
            _context.Add(databaseimage);
            await _context.SaveChangesAsync();
            saved = true;
            return Json(saved ? "Your Snapshot stored in Gallery, you can see it there with your First Name on it" : "image not saved");
        }




        //POST: Delete the snapshot taken by the user
        [HttpPost]
        public async Task<IActionResult> DeleteSnapShot(int? id)
        {
            bool delete = false;
            GalleryImage DeletedImage =  _context.GalleryImage.FirstOrDefault(m => m.Id == id);
            _context.GalleryImage.Remove(DeletedImage);
            await _context.SaveChangesAsync();
            delete = true;
            return Json(delete ? "Your Snapshot deleted from Gallery, ": "image not deleted");
        }





        private Task<ApplicationUser> GetCurrentUserAsync()
        {

            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
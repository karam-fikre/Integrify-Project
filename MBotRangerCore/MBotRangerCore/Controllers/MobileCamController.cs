using Microsoft.AspNetCore.Mvc;

namespace MBotRangerCore.Controllers
{
    public class MobileCamController : Controller
    {

        MbotAppData mobilecamapp;

        public MobileCamController(MbotAppData mobilecamapp)
        {
            this.mobilecamapp = mobilecamapp;

        }

        public IActionResult Index()
        {
            ViewBag.WaitList = mobilecamapp.users;
            //Check if the user Logged in
            bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
               
            }
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace MBotRangerCore.Controllers
{
    public class MobileCamController : Controller
    {
       

        
        public IActionResult Index()
        {
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
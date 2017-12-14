using Microsoft.AspNetCore.Mvc;

namespace MBotRangerCore.Controllers
{
    public class MobileCamController : Controller
    {
       

        
        public IActionResult Index()
        {
            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
               
            }
            return View();
        }
    }
}
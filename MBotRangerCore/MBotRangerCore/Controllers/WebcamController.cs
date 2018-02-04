using Microsoft.AspNetCore.Mvc;

namespace MBotRangerCore.Controllers
{
    public class WebcamController : Controller
    {
        MbotAppData appDatas;

        public WebcamController(MbotAppData appd)
        {
            appDatas = appd;

        }


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


        public IActionResult WebCamMain()
        {           

            //Check if the user Logged in
            bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }            
            return View();
        }

        public IActionResult WebCamMain_ToMock(WebcamController web)
        {
            if (!web.Equals(null))
            {
                return View("WebCamMain");
            }
            return null;
        }



        public IActionResult ReloadCam()
        {
            //Check if the user Logged in
            bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }
            return View("WebCamMain");
        }
    }
}
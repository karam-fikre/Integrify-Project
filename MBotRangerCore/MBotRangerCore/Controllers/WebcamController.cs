using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace MBotRangerCore.Controllers
{
    public class WebcamController : Controller
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
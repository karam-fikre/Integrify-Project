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
            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }

            return View();
        }


        public IActionResult WebCamMain()
        {
            //ViewData["timespent"] = DateTime.Now -DateTime.Now;
            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
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

            
            // DateTime.Now - DateTime.Now;
            //var diffrencebetweentime = DateTime.Now - Convert.ToDateTime(Intial);
            //ViewData["timespent"] = diffrencebetweentime;
           
            return View("WebCamMain");
        }
    }
}
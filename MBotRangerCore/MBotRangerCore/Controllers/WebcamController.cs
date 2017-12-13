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
            
            return View();
        }


        public IActionResult WebCamMain()
        {
            //ViewData["timespent"] = DateTime.Now -DateTime.Now;
            
            

            return View();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MBotRangerCore.Controllers
{
    public class WebcamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReloadCam()
        {
            return View("Index");
        }
    }
}
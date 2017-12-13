using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MBotRangerCore.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;

// Tobis comment #3

namespace MBotRangerCore.Controllers
{
    public class HomeController : Controller
    {
        public DateTime startT = DateTime.Now;
       
        public DateTime endT;

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Start()
        {

            return View();
        }

        public IActionResult About()
        {
            HttpContext.Session.SetString("MyVar", "This is var");
            

            HttpContext.Session.SetString("SVTime", startT.ToString());

            ViewData["Message"] = "Your application description page.";
            HttpContext.Session.SetDouble("Percentage", 75.56);
            HttpContext.Session.SetBoolean("IsIt", false);
            return View();
        }

        public IActionResult Contact()
        {
            endT = DateTime.Now;
            TimeSpan diff = DateTime.Now - startT;
            double seconds = diff.TotalSeconds;
            /*ViewData["timespent"] = endT - startT;*/
            ViewData["Timespent"] = seconds;
            HttpContext.Session.SetString("SVTimeEnd", DateTime.Now.ToString());

            ViewData["Message"] = "Your contact page.";
            ViewData["One"] = HttpContext.Session.GetDouble("Percentage");
            ViewData["Two"] = HttpContext.Session.GetBoolean("IsIt");
            ViewData["Three"] = HttpContext.Session.GetString("SVTime");
            ViewData["Four"] = HttpContext.Session.GetString("SVTimeEnd");

            var culture = new CultureInfo("en-GB");
            HttpContext.Session.SetString("UKTime", DateTime.Now.ToString(culture));
            ViewData["Five"] = HttpContext.Session.GetString("UKTime");
            ViewData["zare"] = DateTime.Now.Year + "/" +
                               DateTime.Now.Month + "/" +
                               DateTime.Now.Day + HttpContext.Session.GetString("MyVar");

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

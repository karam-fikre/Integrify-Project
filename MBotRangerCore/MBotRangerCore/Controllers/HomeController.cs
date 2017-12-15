using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MBotRangerCore.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;



namespace MBotRangerCore.Controllers
{
    public class HomeController : Controller
    {
        public DateTime startT = DateTime.Now;
       
        public DateTime endT;

        MbotAppData _mm;

        public HomeController(MbotAppData mm)
        {
            _mm = mm;
            
        }
        public IActionResult Index()
        {
            
            return View();
        }


        //The Start Page 

        public IActionResult Start()
        {
            // HttpContext.Session.SetString("Type", "0");
            ViewBag.Type = _mm.LoginType;
            return View();
        }

        //public void Session_Start()
        //{
        //    HttpContext.Session.SetInt32("Counter", 1);
        //}

        public IActionResult About()
        {

            //real ones
            ViewData["Status"] = HttpContext.Session.GetInt32("Counter");
          //  Session_Start();
            


            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }

            //real ones
            ViewData["Status"] = HttpContext.Session.GetInt32("Counter");
            Session_Start();
            return View();

          
            /*
            HttpContext.Session.SetString("MyVar", "This is var");
            HttpContext.Session.SetString("SVTime", startT.ToString());
            ViewData["Message"] = "Your application description page.";
            HttpContext.Session.SetDouble("Percentage", 75.56);
            HttpContext.Session.SetBoolean("IsIt", false);
            return View();
            */
        }



        public IActionResult Contact()
        {

            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }

            ViewData["Status"] = HttpContext.Session.GetInt32("Counter");
            //real ones
            if (HttpContext.Session.GetInt32("Counter") == 0)
            {
                HttpContext.Session.SetInt32("Counter", 1);

                return View();
            }
            else if (HttpContext.Session.GetInt32("Counter") == 1)
            {
                HttpContext.Session.SetInt32("Counter", 0);
                ViewData["Wait"] = "In Use";
                return View("About");
            }
            else

            {
                HttpContext.Session.SetInt32("Counter", 1);
                return View("About");
            }


            //older


            /*


           


            endT = DateTime.Now;
            TimeSpan diff = DateTime.Now - startT;
            double seconds = diff.TotalSeconds;
            //ViewData["timespent"] = endT - startT;
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

            return View();*/
        }




        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

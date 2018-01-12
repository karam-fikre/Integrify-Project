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
        MbotAppData homeAppData;

        public HomeController(MbotAppData _homeAppData)
        {
            homeAppData = _homeAppData;
            
        }
        public IActionResult Index()
        {
            return View();
        }


        //The Start Page 

        public IActionResult Start()
        {
            ViewBag.TimerLog = homeAppData.TimerForLogout;
            ViewBag.Type = homeAppData.LoginType;
            ViewBag.WaitList = homeAppData.users;

            return View();            
        }

        //public void Session_Start()
        //{
        //    HttpContext.Session.SetInt32("Counter", 1);
        //}

        public IActionResult About()
        {
            ViewBag.WaitList = homeAppData.users;
            ViewBag.TimerLog = homeAppData.TimerForLogout;
            ViewBag.Session = HttpContext.Session.GetString("User");
            //Check if the user logged in
            bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (!IsAuthenticated)

            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }

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
            ViewBag.WaitList = homeAppData.users;
            string allT = "";

            foreach (LoginViewModel lls in homeAppData.users)

            {
                allT = allT + " " + lls.Email;
            }

            ViewBag.Lists = allT;

            //Check if the user Logged in
            bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
{
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }     

            
            

            return View();
           

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

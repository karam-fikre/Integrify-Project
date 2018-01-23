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

        MbotAppData homeAppData;

        public HomeController(MbotAppData _homeAppData)
        {
            homeAppData = _homeAppData;
            
        }

        public IActionResult Index()
        {
            ViewBag.WaitList = homeAppData.users;
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
            //Check if the user logged in
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }

            ViewBag.WaitList = homeAppData.users;
            ViewBag.TimerLog = homeAppData.TimerForLogout;
            ViewBag.Session = HttpContext.Session.GetString("User");
            return View();
        }
        
        public IActionResult Contact()
        {
            //Check if the user Logged in
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }

            ViewBag.WaitList = homeAppData.users;
            return View();      
        
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using MBotRangerCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

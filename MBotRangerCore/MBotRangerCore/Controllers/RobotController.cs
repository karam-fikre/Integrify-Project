using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Sockets;
using System.Net;
using MBotRangerCore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MBotRangerCore.Controllers
{
    public class RobotController : Controller
    {
        public byte[] sendbuf;

        MbotAppData robotAppData;
    

        public RobotController(MbotAppData robotAppData)
        {
            this.robotAppData = robotAppData;
        }

        [HttpPost]
        public string MoveRobotOption(string option)
        {
            string distan = robotAppData.Distance;
            if (!String.IsNullOrEmpty(option))
            {
                AssignToArduino(option);
                return distan;
            }
            return "Unsuccesful";
        }

        [HttpPost]
        public string MoveRobotArrowsOption(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                RobotArrows(str);
                return str;
            }
            return "Unsuccesful";

        }

     
        [SessionTimeOut(1)]
        public IActionResult Index(string submit)
        {

            string loggedInUser      = HttpContext.Session.GetString("User");
            string mainUser          = robotAppData.CurrentUser; //The user who has the access to control the robot
            bool isUserSameAsCurrent = !String.IsNullOrEmpty(loggedInUser) && 
                                       !String.IsNullOrEmpty(mainUser) &&
                                       loggedInUser.Equals(mainUser);
            //Check if the user Logged in
 			bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (!IsAuthenticated  || !isUserSameAsCurrent)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }

            
            ViewBag.WaitList = robotAppData.users;
          ViewBag.dis=  robotAppData.Distance;
            AssignToArduino(submit);
            return View();
        }


        public IActionResult RobotArrows(string str)
        {
            if (!string.IsNullOrEmpty(str))

            {
               //Check if the user Logged in
           		bool IsAuthenticated = User.Identity.IsAuthenticated;
            	if (!IsAuthenticated)
                {
                    return RedirectToAction(nameof(HomeController.Start), "Home");

                }

                ViewData["Key"] = str;
                AssignToArduino(str);
            }

            return View();
        }

       
        public IActionResult Mouse(string submit)
        {
            //Check if the user Logged in
            bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }
            AssignToArduino(submit);
            return View();
        }

        public void AssignToArduino(string option)
        {
            if (!String.IsNullOrEmpty(option))
            {
                byte[] data = new byte[1024];
                string stringData;
                UdpClient server = new UdpClient("195.198.161.214", 80);

                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                data = Encoding.ASCII.GetBytes(option);
                server.Send(data, data.Length);
                data = server.Receive(ref sender);
                stringData = Encoding.ASCII.GetString(data, 0, data.Length);
                robotAppData.Distance = stringData;


            }
        }

        public IActionResult Reload()
        {
            //Check if the user Logged in
            bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }
            //DateTime.Now - DateTime.Now;
            //var diffrencebetweentime = DateTime.Now - Convert.ToDateTime(Intial);
            //ViewData["timespent"] = diffrencebetweentime;
            return View("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public bool ForXUnit()
        {
            return true;
        }

        public IActionResult ForXUnit2(RobotController result)
        {
            if (!result.Equals(null))
            {
                return View("Index");
            }
            return null;
        }

        public virtual IActionResult ForXUnitIndex(string str)
        {
            return null;
        }


    }
}
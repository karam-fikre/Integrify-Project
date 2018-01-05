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
            if (!String.IsNullOrEmpty(option))
            {
                AssignToArduino(option);
                return option;
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

        public void ShowWaitingList()
        {
           /* string all_U = "";
            foreach (var allUsers in robotAppData.users)
            {
                all_U = all_U + "\n | " + allUsers.Email + " |";
            }

            ViewBag.TheList = all_U;*/
            ViewBag.WaitList = robotAppData.users;
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


            ShowWaitingList();
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
                sendbuf = Encoding.ASCII.GetBytes(option);

                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                 ProtocolType.Udp);
                IPAddress broadcast = IPAddress.Parse("195.198.161.214");
                IPEndPoint ep = new IPEndPoint(broadcast, 80);
                s.SendTo(sendbuf, ep);
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
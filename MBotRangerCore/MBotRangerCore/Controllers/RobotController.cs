using MBotRangerCore.Helpers;
using MBotRangerCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace MBotRangerCore.Controllers
{
    public class RobotController : Controller
    {
        ConfirmViewModel rob = new ConfirmViewModel();
        public bool isViewPublic = false;
        public byte[] sendbuf;
        MbotAppData robotAppData;
        WaitingUsers waitListObj = new WaitingUsers();

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
                AssignToArduino(str);

                return str;
            }
            return "Unsuccesful";

        }

        


        [SessionTimeOut(1)]
        public IActionResult Index(string submit, bool isPublic)
        {
            ViewBag.GuestWaitTime = waitListObj.GetWaitingTimeInSeconds(robotAppData.users);
            

            //Check if the user Logged in
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }

            ViewBag.Public = "No";
            string loggedInUser = HttpContext.Session.GetString("User");
            string mainUser = robotAppData.CurrentUser; //The user who has the access to control the robot
            bool isUserSameAsCurrent = !String.IsNullOrEmpty(loggedInUser) &&
                                       !String.IsNullOrEmpty(mainUser) &&
                                       loggedInUser.Equals(mainUser);

            //The user is not main user.
            if (!isUserSameAsCurrent)
            {
                rob.IsWaitingUser = true;
                ViewBag.Public = (robotAppData.IsRobotVideoPublic) ? "Yes" : "No";

                ViewBag.GuestWaitTime = waitListObj.GetWaitingTimeInSeconds(robotAppData.users);
            }
            //Only the main user can change from public to private or vise versa
            else
            {
                //rob.IsWaitingUser = false;
                robotAppData.IsRobotVideoPublic = isPublic;
                ViewBag.Public = (robotAppData.IsRobotVideoPublic) ? "Yes" : "No";
                ConstructorAssigner(robotAppData);
            }


            //Assign logout time based on the number of users. 
            //For instance, if there is only one user, the user should have access as long as he is not idle for too long for instance
            //Orginal          robotAppData.TimerForLogout = waitListObj.getLogoutTime(robotAppData.users.Count);
            //TEMP Edited the way to get the timeLogout temporary

             
            ViewBag.NoOF_Users = robotAppData.users.FindIndex(a => a.Email == loggedInUser);
            robotAppData.TimerForLogout = waitListObj.getLogoutTime(robotAppData.users, robotAppData.users.Count);

            ViewBag.WaitList = robotAppData.users;

            AssignToArduino(submit);
           
            //ViewBag.Time = waitListObj.usersTime[robotAppData.users[0].ToString()];
            return View(rob);            
        


        }

        public void ConstructorAssigner(MbotAppData theAppData)
        {
            new HomeController(theAppData);
            new WebcamController(theAppData);
            new RobotController(theAppData);
            new SessionTimeOutAttribute(theAppData, false);
        }

        public IActionResult RobotArrows(string str)
        {
            //Check if the user Logged in
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }

            if (!string.IsNullOrEmpty(str))
            {
                ViewData["Key"] = str;
                AssignToArduino(str);
            }
            return View();
        }


        public IActionResult Mouse(string submit)
        {
            //Check if the user Logged in
            if (!User.Identity.IsAuthenticated)
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
                s.Close();
            }
        }

        public IActionResult Reload()
        {
            //Check if the user Logged in
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }
            return View("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



#region XUnit-Methods



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

        #endregion

       
    }
}
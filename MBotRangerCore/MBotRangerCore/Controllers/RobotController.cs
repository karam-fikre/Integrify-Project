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
using MBotRangerCore.Helpers;
using System.Text.RegularExpressions;
using System.IO;

namespace MBotRangerCore.Controllers
{
    public class RobotController : Controller
    {
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
                //  RobotArrows(str);
                // AssignToArduino(str);
                return str;
            }
            return "Unsuccesful";

        }



        public List<LoginViewModel> MyAction()
        {

            return robotAppData.users;
        }
        ConfirmViewModel rob = new ConfirmViewModel();
        //public IActionResult ISPublic(bool isPublic)
        //{

        //ViewBag.IsPublic = isPublic;
        //    bool ff = ViewBag.IsPublic;
        //    rob.Is_Public = isPublic;
        //    return View("Index", rob);
        //}

        [SessionTimeOut(1)]
        public IActionResult Index(string submit, bool isPublic)
        {
            ViewBag.YouWait = waitListObj.GetWaitingTimeInSeconds(robotAppData.users);
            ViewBag.NoOF_Users = (robotAppData.users.Count) - 2;

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
                // ViewBag.YouWait = waitListObj.GetTimeDifference(robotAppData.users,robotAppData.users[1].LoggedInTime);
                ViewBag.YouWait = waitListObj.GetWaitingTimeInSeconds(robotAppData.users);


            }
            //Only the main user can change from public to private or vise versa
            else
            {
                robotAppData.IsRobotVideoPublic = isPublic;
                ViewBag.Public = (robotAppData.IsRobotVideoPublic) ? "Yes" : "No";
                ConstructorAssigner(robotAppData);

            }


            //Assign logout time based on the number of users. 
            //For instance, if there is only one user, the user should have access as long as he is not idle for too long for instance
            //Orginal          robotAppData.TimerForLogout = waitListObj.getLogoutTime(robotAppData.users.Count);
            //TEMP Edited the way to get the timeLogout temporary
            robotAppData.TimerForLogout = waitListObj.getLogoutTime(robotAppData.users, robotAppData.users.Count);

            ViewBag.TimerLog = robotAppData.TimerForLogout;
            ViewBag.WaitList = robotAppData.users;
            AssignToArduino("0");




            //ViewBag.Time = waitListObj.usersTime[robotAppData.users[0].ToString()];
            return View(rob);

            //Orginal before Monday is here down
            /*
 			bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (!IsAuthenticated  || !isUserSameAsCurrent)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }
            if (robotAppData.users.Count > 1)
            {
                robotAppData.TimerForLogout = 10000;
            }
            else
            {
                robotAppData.TimerForLogout = 100031000;
            }
            ViewBag.TimerLog = robotAppData.TimerForLogout; 
            ViewBag.WaitList = robotAppData.users;
            AssignToArduino("0");
            return View(rob);
            */
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


        #region XUnit Action/Methods

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








        [HttpPost]
        public async Task<IActionResult> SaveSnapshot()
        {
            bool saved = false;
           
            string image = Request.Form["datatype"].ToString();
            var base64Data = Regex.Match(image, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);
            // var data = Convert.FromBase64String(image);
            var path = Path.GetTempFileName();
            // var path = Path.Combine(, "snapshot.png");
            //  var uploads = Path.Combine(_appEnv.WebRootPath, path);
            using (var stream = new FileStream(path, FileMode.Create))
            {
              await binData.CopyToAsync(stream);
            }
            System.IO.File.WriteAllBytes(path, binData);
            saved = true;


            return Json(saved ? "image saved" : "image not saved");
        }
    }
}
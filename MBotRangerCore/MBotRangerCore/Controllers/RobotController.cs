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
        public IActionResult Some(string submit)
        {

            return RedirectToAction("Index");
            //do something
        }

        [HttpPost]
        public string MoveRobotOption(string option)
        {
            if (!String.IsNullOrEmpty(option))
            {
                switch (option)
                {
                    case "1":
                        sendbuf = Encoding.ASCII.GetBytes("1");
                        break;
                    case "2":
                        sendbuf = Encoding.ASCII.GetBytes("2");
                        break;
                    case "3":
                        sendbuf = Encoding.ASCII.GetBytes("3");
                        break;
                    case "4":
                        sendbuf = Encoding.ASCII.GetBytes("4");
                        break;
                    case "5":
                        sendbuf = Encoding.ASCII.GetBytes("5");
                        break;
                    default:
                        sendbuf = Encoding.ASCII.GetBytes("0");
                        break;
                }

                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                  ProtocolType.Udp);

                IPAddress broadcast = IPAddress.Parse("192.168.4.1");
                IPEndPoint ep = new IPEndPoint(broadcast, 1025);
                s.SendTo(sendbuf, ep);

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


        [SessionTimeOut(1)]
        public IActionResult Index(string submit)
        {

            string ss1 = HttpContext.Session.GetString("Current");
            string ss2 = robotAppData.CurrentUser;
            bool areUserEqual = !String.IsNullOrEmpty(ss1) && 
                                !String.IsNullOrEmpty(ss2) && 
                                ss1.Equals(ss2);

            TempData["robV"] = "1";


            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa || !areUserEqual)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }
            //Button Options
            switch (submit)
            {
                case "1":
                    sendbuf = Encoding.ASCII.GetBytes("1");
                    break;
                case "2":
                    sendbuf = Encoding.ASCII.GetBytes("2");
                    break;
                case "3":
                    sendbuf = Encoding.ASCII.GetBytes("3");
                    break;
                case "4":
                    sendbuf = Encoding.ASCII.GetBytes("4");
                    break;
                case "5":
                    sendbuf = Encoding.ASCII.GetBytes("5");
                    break;
                default:
                    sendbuf = Encoding.ASCII.GetBytes("0");
                    break;
            }

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
              ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.4.1");
            IPEndPoint ep = new IPEndPoint(broadcast, 1025);
            s.SendTo(sendbuf, ep);
            
            return View();
        }


        public IActionResult RobotArrows(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                bool aaa = User.Identity.IsAuthenticated;
                if (!aaa)
                {
                    return RedirectToAction(nameof(HomeController.Start), "Home");

                }
                ViewData["Key"] = str;
                sendbuf = Encoding.ASCII.GetBytes(str);

                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                  ProtocolType.Udp);

                IPAddress broadcast = IPAddress.Parse("192.168.4.1");
                IPEndPoint ep = new IPEndPoint(broadcast, 1025);
                s.SendTo(sendbuf, ep);

            }

            return View();
        }
        public IActionResult Mouse(string submit)
        {
            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }
            switch (submit)
            {
                case "Forward":
                    sendbuf = Encoding.ASCII.GetBytes("1");
                    break;
                case "Back":
                    sendbuf = Encoding.ASCII.GetBytes("2");
                    break;
                case "Go Left":
                    sendbuf = Encoding.ASCII.GetBytes("3");
                    break;
                case "Go Right":
                    sendbuf = Encoding.ASCII.GetBytes("4");
                    break;
                case "Stop":
                    sendbuf = Encoding.ASCII.GetBytes("5");
                    break;
                default:
                    sendbuf = Encoding.ASCII.GetBytes("0");
                    break;
            }

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
              ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.4.1");
            IPEndPoint ep = new IPEndPoint(broadcast, 1025);
            s.SendTo(sendbuf, ep);
            return View();
        }

        public IActionResult Reload()
        {
            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
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
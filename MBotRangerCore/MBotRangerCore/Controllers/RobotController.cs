using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace MBotRangerCore.Controllers
{
    public class RobotController : Controller
    {
        public byte[] sendbuf;
        public IActionResult Index(string submit)
        {

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


        public ActionResult RobotArrows(int? id)
        {
            ViewData["Key"] = id.ToString();
            sendbuf = Encoding.ASCII.GetBytes(id.ToString());

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
              ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.4.1");
            IPEndPoint ep = new IPEndPoint(broadcast, 1025);
            s.SendTo(sendbuf, ep);

            return View();
        }
    }
}
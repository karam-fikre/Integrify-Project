using MBotRangerCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBotRangerCore
{
    public class MbotAppData 
    {
        public bool IsItInUse { get; set; } = false;             // Is there anyone accessing the robot
        public int LoggedInCounter { get; set; } = 0;           //  Handles how many users are there.
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }
        public bool LoginType { get; set; } = false;   //Guest or logged in Mode
        public List<LoginViewModel> users = new List<LoginViewModel>();
        public string CurrentUser { get; set; }
        public int TimerForLogout { get; set; } = 1500055000;
        public bool IsRobotVideoPublic { get; set; } = false;
    }
}

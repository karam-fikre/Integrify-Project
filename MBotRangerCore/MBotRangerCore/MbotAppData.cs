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
        public bool InUse = false;               // Is there anyone accessing the robot
        public int Counter = 0;                 //  Handles how many users are there.
        public DateTime StartTime = DateTime.Now;
        public DateTime EndTime = DateTime.Now;  
        public int LoginState = 0;             //How to show the start page
        public bool LoginType { get; set; } = false;
        public List<LoginViewModel> user=new List<LoginViewModel>();
       
        
        
    }
}

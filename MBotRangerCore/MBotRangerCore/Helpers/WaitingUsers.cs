using MBotRangerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBotRangerCore.Helpers
{
    public class WaitingUsers
    {
        public Dictionary<string, int> usersLogOutTime;
        public Dictionary<string, DateTime> usersTime;

        public WaitingUsers()
        {
           usersLogOutTime = new Dictionary<string, int>();
           usersTime = new Dictionary<string, DateTime>();
        }

        public int getLogoutTime(int no_OfUsers)
        {
            if (no_OfUsers > 1)
                return 100000000; //in Miliseconds;
            else
                return 300000000;
        }

        
        internal int getLogoutTime(List<LoginViewModel> users, int countUsers)
        {
            int index = users.FindIndex(a => a.Email == a.Email);
            if (index == 0)
            {
                if (countUsers > 1) 
                    return (5*60*1000); //First user when there are other users waiting
                else
                    return 700000; //Only first user is in the list
            }
            return 500000500; // Guest users
        }

        public int GetTimeDifference(List<LoginViewModel> users, DateTime userTime)
        {            
            TimeSpan timeDiff = userTime - Convert.ToDateTime(users[0].LoggedInTime);
            int milliSecondsDiff = (int)timeDiff.TotalMilliseconds;
            return milliSecondsDiff;
        }

   			 public int GetWaitingTimeInSeconds(List<LoginViewModel> users)

        {
            int seconds = 79999;
            if (users.Count == 1)
            {
               
                DateTime user1 = users[0].LoggedInTime;
                //If only one user is active, assign 10 hours
                user1 = user1.AddHours(10);
               
               
                DateTime assignTime = new DateTime( user1.Year,
                                                    user1.Month,
                                                    user1.Day,
                                                    user1.Hour , 
                                                    user1.Minute,
                                                    user1.Second,
                                                    user1.Millisecond,
                                                    DateTimeKind.Local);


                DateTime dt2Now = DateTime.Now;

                TimeSpan result = assignTime.Subtract(dt2Now);

                seconds = Convert.ToInt32(result.TotalSeconds);
                if (seconds <= 0)
                    return 0;
            }
            else if (users.Count > 1)
            {
                DateTime user2 = users[1].LoggedInTime;
                //if there are more than one user, assign 5 minutes from the second user signed in
               // user2 = user2.AddMinutes(5);
                user2 = user2.AddMinutes(59);
                // user2 = user2.AddSeconds(25);

                DateTime assignTime = new DateTime( user2.Year,
                                                    user2.Month,
                                                    user2.Day,
                                                    user2.Hour,
                                                    user2.Minute,
                                                    user2.Second,
                                                    user2.Millisecond, 
                                                    DateTimeKind.Local);

                DateTime dtNow = DateTime.Now;

                TimeSpan result = assignTime.Subtract(dtNow);

                seconds = Convert.ToInt32(result.TotalSeconds);
                if (seconds <= 0)
                    return 0;
            }

            return seconds;

        }
    }
}

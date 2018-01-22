using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBotRangerCore.Models
{
    public static class WaitingList
    {
        private static List<WaitingRecord> waitList = new List<WaitingRecord>();

        //
        public static IEnumerable<WaitingRecord> WaitList
        {
            get { return waitList; }
        }

        public static bool AddUser(string userName)
        {
            if (waitList.Any(p => userName == p.UserName))
                return false; // User is already in waiting list. Could be an error from elsewhere.

            WaitingRecord wr = new WaitingRecord
            {
                UserName = userName,
                DateTimeLastActive = DateTime.Now
            };

            waitList.Add(wr);
            return true; // User is properly added last in the list.
        }

        public static bool RemoveUser(string userName)
        // This method removes a user from the waiting list.
        // If user not found, then return false as a signal that the user didn't exist. Could be an error from elsewhere.
        // If the user is removed without problems, then return true.
        {
            const int notFoundInList = -1;
            int positionInList = FindUserInList(userName);
            if (positionInList == notFoundInList) return false;

            waitList.RemoveAt(positionInList);
            return true;
        }

        public static int Count()
        {
            return waitList.Count();
        }

        public static bool Touch(string userName)
        {
            //Todo: Check for errors...
            const int notFoundInList = -1;
            int positionInList = FindUserInList(userName);
            if (positionInList == notFoundInList) return false;

            WaitingRecord wr = new WaitingRecord
            {
                UserName = userName,
                DateTimeLastActive = DateTime.Now
            };
            waitList.RemoveAt(positionInList);
            waitList.Insert(positionInList, wr);
            return true;
        }

        public static bool UserStillActive(string userName)
        // Gives true if user has done something in the last 30 seconds.
        // Gives false if user has done nothing during the last 30 seconds.
        // Don't change anything in the record!
        {
            const int secs = 30; // 30 seconds as inactivity time
            const int notFoundInList = -1;
            int positionInList = FindUserInList(userName);
            if (positionInList == notFoundInList)
                return false;

            DateTime timeLastActive = waitList[1].DateTimeLastActive.AddSeconds(secs);
            if (timeLastActive > DateTime.Now)
                return true; // Yes, the user is still active.
            return false; // No, the user has been inactive for n seconds.
        }

        public static void RemoveAllInactiveUsers()
        // This is an inefficient method, 
        // because I start from the beginning every time I've removed a user.
        // If I just traverse one time, there will be users I don't test.
        {
            while (true)
            {
                foreach (WaitingRecord wr in waitList)
                {
                    if (!UserStillActive(wr.UserName))
                    {
                        RemoveUser(wr.UserName);
                        break;
                    }
                }
                continue;
            }
        }

        public static string WhoIsRunningRobot()
        {
            if (waitList.Count() == 0)
                return ""; // Noone is running the robot right now. The robot is inactive.
            return waitList[0].UserName;
        }

        public static List<(string, string)> UsersInWaitingList()
        {
            // This method returns a list of tuples.
            // Each tuple has two items: (1) user name and (2) time when he was latest active.
            List<(string, string)> list = new List<(string, string)>();
            for (int i = 0; i < waitList.Count; i++)
            {
                list.Add((waitList[i].UserName, waitList[i].DateTimeLastActive.ToLongTimeString()));
            }
            return list;
        }

        private static int FindUserInList(string userName)
        {
            const int userNotFoundInList = -1;
            int i = 0;
            foreach (WaitingRecord wr in waitList)
            {
                if (wr.UserName == userName)
                {
                    return i; // Index at where the user is found in list.
                }
                i++;
            }
            return userNotFoundInList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitMbot
{
    public class FakeRobotController
    {

        byte[] sendbuf;
        public byte[] Robot(string submit)
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



            return sendbuf;
        }


        public virtual bool Check()
        {
            return false;
        }
    }

    public class FakeRobotContoller_Child
    {
        public int CheckCaller(FakeRobotController obj)
        {
            if (obj.Check())
            {
                return 1;
            }
            return 0;            
        }
    }
}

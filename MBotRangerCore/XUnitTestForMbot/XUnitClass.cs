using System;
using Xunit;
using MBotRangerCore.Controllers;
using System.Text;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace XUnitMbot
{
    public class XUnitClass
    {
        [Fact]
        public void XTest1()
        {
            RobotController robot = new RobotController();
            Assert.True(robot.ForXUnit());
        }
        [Fact]
        public void testFakeMethod()
        {
            FakeClass fk = new FakeClass();
            Assert.True(fk.FakeMethod());
        }

        [Fact]
        //Testing a FakeRobotController Action
        public void TestFakeRobotAction()
        {
            var fakeCon = new FakeRobotController();
            byte[] byte1, byte2;
            byte1 = fakeCon.Robot("Forward");
            byte2 = Encoding.ASCII.GetBytes("1");
            bool bothEqual = Enumerable.SequenceEqual(byte1, byte2);
            Assert.True(bothEqual);
        }

        [Fact]
        //Testing mocking on FakeRobotController
        public void TestFakeRobot_Mock()
        {
            Mock<FakeRobotController> fakeObj = new Mock<FakeRobotController>();
            fakeObj.Setup(x => x.Check()).Returns(true);

            FakeRobotContoller_Child fakeChildObj = new FakeRobotContoller_Child();
            int actual = fakeChildObj.CheckCaller(fakeObj.Object);
            Assert.Equal(1, actual);

        }

        //Testing mocking on Fake Index inside Robot Controller
        [Fact]
        public void TestRobotIndex_Mock()
        {
            var result = new ViewResult();
            Mock<RobotController> robotMock = new Mock<RobotController>();
            robotMock.Setup(x => x.ForXUnitIndex("")).Returns(result);
            var controller = new RobotController();
            var expected = controller.ForXUnit2(robotMock.Object);
            Assert.NotNull(expected);

        }

        [Fact]
        //Testing if the Robot Index action returns a non null view
        public void TestNotNullRobotIndex()
        {
            var robCon = new RobotController();
            ViewResult result = robCon.Index("") as ViewResult;
            Assert.NotNull(result);
        }


        [Fact]
        //Testing if the Relaodcam Action returns expected view
        public void TestWebcamReload()
        {
            var webcamCon = new WebcamController();
            ViewResult result = webcamCon.ReloadCam() as ViewResult;
            Assert.Equal(result.ViewName, "Index");
        }

        [Fact]
        //Testing if the Robot RobotArrows action returns a non null view
        public void TestNotNullRobotArrows()
        {
            var controller = new RobotController();
            ViewResult result = controller.RobotArrows(1) as ViewResult;
            Assert.NotNull(result);
        }

    }
}

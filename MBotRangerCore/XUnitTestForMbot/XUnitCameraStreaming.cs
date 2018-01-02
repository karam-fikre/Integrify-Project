using System;
using Xunit;
using MBotRangerCore.Controllers;
using System.Text;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace XUnitMbot
{
    public class XUnitCameraStreaming
    {
        [Fact]
        public void testTrue()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestNotNullWebCamMain()
        {
            WebcamController webCon = new WebcamController(null);
            ViewResult result = webCon.WebCamMain() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestNotNullMobileCam()
        {
            var mobcam = new MobileCamController();
            ViewResult result = mobcam.Index() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        //Testing if the Relaodcam Action returns expected view
        public void TestWebcamReload()
        {
            var webcamCon = new WebcamController(null);
            ViewResult result = webcamCon.ReloadCam() as ViewResult;
            Assert.Equal(result.ViewName, "WebCamMain");
        }

        [Fact]
        public void TestWebCamMain_Mock()
        {
            var result = new ViewResult();
            Mock<WebcamController> webcamMock = new Mock<WebcamController>();
            webcamMock.Setup(x => x.WebCamMain()).Returns(result);
            var controller = new WebcamController(null);
            var expected = controller.WebCamMain_ToMock(webcamMock.Object);
            Assert.NotNull(expected);

        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace MBotRangerCore.Controllers
{
    public class WebcamController : Controller
    {

        MbotAppData appDatas;

        public WebcamController(MbotAppData appd)
        {
            appDatas = appd;

        }

        public IActionResult Index()
        {
            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }

            return View();
        }


        public IActionResult WebCamMain()
        {
            //ViewData["timespent"] = DateTime.Now -DateTime.Now;
            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa || appDatas.LoginState!=0) //more than 1 user go to Home
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }
            appDatas.LoginState = 1;
            return View();
        }

        public IActionResult WebCamMain_ToMock(WebcamController web)
        {
            if (!web.Equals(null))
            {
                return View("WebCamMain");
            }
            return null;
        }



        public IActionResult ReloadCam()
        {
            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }

            // DateTime.Now - DateTime.Now;
            //var diffrencebetweentime = DateTime.Now - Convert.ToDateTime(Intial);
            //ViewData["timespent"] = diffrencebetweentime;

            return View("WebCamMain");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBotRangerCore.Models;
using WebApplication7;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using MBotRangerCore;

namespace MBotRangerCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession ourSession => _httpContextAccessor.HttpContext.Session;
     

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly MbotAppData _mData;

        public AccountController(

            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            MbotAppData mData
            )
        {
            _mData = mData;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            
        }


        [TempData]
        public string ErrorMessage { get; set; }





        //GET: get the User Account Inforamtion

        [HttpGet]
        public IActionResult Index()
        {

            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }

            HttpContext.Session.SetInt32("Counter", 0);
            

            return View();
        }

        // GET: Users/Login
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["HowMany"] = _mData.Counter;
       

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
             ViewData["ReturnUrl"] = returnUrl;
             return View();             
        }





        // POST: Users/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["HowMany"] = _mData.Counter;
            
           // _mData.user.Add(new LoginViewModel() { UserId =0});
            //if (HttpContext.Session.GetInt32("Counter") == 0)
            if (!_mData.InUse && _mData.user.Count==0)
            {
                HttpContext.Session.SetInt32("Counter", 1);
                
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        // ViewBag.isUsed = true;
                        _mData.LoginState = 1;
                        _mData.Counter++;
                        _mData.InUse = true;

                        _logger.LogInformation("User logged in.");
                        return RedirectToAction(nameof(WebcamController.WebCamMain), "Webcam");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View();
                    }
                }
                return View();
            }
            else
            {
                HttpContext.Session.SetString("Type", "1");
                _mData.LoginType = true;
                new HomeController(_mData);
                ModelState.AddModelError(string.Empty, "Someone has logged in");
                _mData.Counter++;
                return RedirectToAction(nameof(HomeController.Start), "Home");
            }     
           
            //return View();           
           
        }





        // GET: Users/Register

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }





        // POST: Users/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ConfirmViewModel model, string returnUrl = null)
        {
            List<string> _allowedEmailDomains = new List<string> { "outlook.com", "hotmail.com", "gmail.com", "yahoo.com" };
            var emailDomain = model.Email.Split('@')[1];

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (!_allowedEmailDomains.Contains(emailDomain.ToLower()))
                {
                    ModelState.AddModelError(string.Empty, "Email domain is not allowed it's should be one of these (gmail,yahoo,outlock,hotmail)");
                    return View();
                }
                var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, DateOfBirth = model.DateOfBirth, Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    _mData.InUse = true;
                    return RedirectToAction(nameof(WebcamController.WebCamMain), "Webcam");
                }
                AddErrors(result);
            }

            return View(model);
        }





        //Logout

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            _mData.InUse = false;
            _mData.Counter--;
            return RedirectToAction(nameof(AccountController.Login));
            
        }





        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}

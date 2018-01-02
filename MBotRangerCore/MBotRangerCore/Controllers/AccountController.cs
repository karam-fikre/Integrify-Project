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
        //Session accessor variables
        const string S_counter = "counter";
        const string S_state = "state";
        const string S_tester = "test";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession ourSession => _httpContextAccessor.HttpContext.Session;
        //public AccountController(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly MbotAppData mBotAppVar;

        public AccountController(

            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            MbotAppData _mBotAppVar
            )
        {
            mBotAppVar = _mBotAppVar;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            
        }


        [TempData]
        public string ErrorMessage { get; set; }


        public void ConstructorsAssigner(MbotAppData theAppData)
        {
            new HomeController(theAppData);
            new WebcamController(theAppData);
            new RobotController(theAppData);
            new SessionTimeOutAttribute(theAppData, false);
        }


        //GET: get the User Account Inforamtion

        [HttpGet]
        public IActionResult Index()
        {

            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }

            HttpContext.Session.SetInt32(S_counter, 0);
            

            return View();
        }

        // GET: Users/Login
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["HowMany"] = mBotAppVar.LoggedInCounter;
            // HttpContext.Session.SetInt32("Counter", 0);
            /* 

             if (HttpContext.Session.GetInt32("Counter") == 0)
             {
                 ViewData["Status"] = "No Logged in User";
             }
             else
             {
                 ViewData["Status"] = "The Page is in Use";
             }
             */
            /* if (ViewBag.isUsed)
             {
                 return RedirectToAction(nameof(AccountController.Login));
             }
             else
             {
                 await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                 ViewData["ReturnUrl"] = returnUrl;
                 return View();

               }*/

            // Clear the existing external cookie to ensure a clean login process

            /* if (TempData["LLL"]!=null)
             {
                 _logger.LogInformation("User logged in.");
                 return RedirectToAction(nameof(RobotController.Index), "Robot");


             }
             else
             {
                 await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                 ViewData["ReturnUrl"] = returnUrl;
                 return View();
             }*/

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
            ViewData["HowMany"] = mBotAppVar.LoggedInCounter;     
                
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {                    
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                      if (!mBotAppVar.IsItInUse)
                         {
                        mBotAppVar.testList.Add("added");
                        mBotAppVar.users.Add(new LoginViewModel(){ Email = model.Email });
                        HttpContext.Session.SetString("Current", model.Email);
                        mBotAppVar.CurrentUser = model.Email;
                        ////App variables

                        //mBotAppVar.users.Add(new LoginViewModel() { UserId =0});
                        mBotAppVar.LoggedInCounter++;
                        mBotAppVar.IsItInUse = true;
                        ConstructorsAssigner(mBotAppVar);
                        
                        //     //Session variables
                        //     TempData["LLL"] = "yes";
                        //     HttpContext.Session.SetInt32(S_counter, 1);
                        // ViewBag.isUsed = true;                   

                        _logger.LogInformation("User logged in.");
                             return RedirectToAction(nameof(RobotController.Index), "Robot");
                        }
                    else
                    {
                        mBotAppVar.testList.Add("AddedGuest");
                        mBotAppVar.users.Add(new LoginViewModel() { UserId = model.UserId });
                        HttpContext.Session.SetString("Current", model.Email);

                        //App variables

                        mBotAppVar.LoggedInCounter++;
                        /*mBotAppVar.LoginType = true;
                        ConstructorsAssigner(mBotAppVar);
                        //Session variables
                        HttpContext.Session.SetString(S_state, "1"); */

                        ModelState.AddModelError(string.Empty, "Someone has logged in");                        
                        return RedirectToAction(nameof(HomeController.Start), "Home");
                    }
                }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View();
                    }
                }
                return View();
               
           
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
                    if (!mBotAppVar.IsItInUse)
                    {
                        HttpContext.Session.SetString("Current", model.Email);
                        mBotAppVar.CurrentUser = model.Email;
                        mBotAppVar.IsItInUse = true;
                        ConstructorsAssigner(mBotAppVar);
                        // TODO: id FSDAFDSA
                        mBotAppVar.LoggedInCounter++;
                        /* //App variables

                         //Session variables
                         TempData["LLL"] = "yes";*/

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created a new account with password.");
                        return RedirectToAction(nameof(RobotController.Index), "Robot");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Current", model.Email);
                        ModelState.AddModelError(string.Empty, "Someone has logged in");
                        return RedirectToAction(nameof(HomeController.Start), "Home");
                    }
                  
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(int? notUsedInt)
        {
            mBotAppVar.IsItInUse = false;
            mBotAppVar.LoggedInCounter--;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
           return RedirectToAction(nameof(AccountController.Login));
        }

        //Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {

            mBotAppVar.IsItInUse = false;
            mBotAppVar.LoggedInCounter--;
            /* //App variables
             mBotAppVar.testList.RemoveAt(0);
             
             
             mBotAppVar.LoginType = false;
             mBotAppVar.LoginState = 0;
            // mBotAppVar.users.RemoveAt(mBotAppVar.users.Count-1);
             //Session variables
             TempData["LLL"] = null;*/
            //if (mBotAppVar.users.Count == 0)
            //{
            await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");

                return RedirectToAction(nameof(AccountController.Login));
            //}

            //else
            //{
            //    LoginViewModel ll = new LoginViewModel();
            //    await Login(ll, null);
            //    return null;
            //}

            

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

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
        public string wholeAccess="";
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

        public AccountController( UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  ILogger<AccountController> logger,
                                  MbotAppData _mBotAppVar )
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
            HttpContext.Session.SetInt32(S_counter, 0);

            bool aaa = User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return RedirectToAction(nameof(HomeController.Start), "Home");

            }
            return View();
        }

        // GET: Users/Login        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["HowMany"] = mBotAppVar.LoggedInCounter; 

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
                        ViewBag.CountDown = 0;
                        // mBotAppVar.testList.Add(model.Email.ToString());                        
                        mBotAppVar.users.Add(new LoginViewModel() { Email = model.Email });                       
                        HttpContext.Session.SetString("User", model.Email);
                        HttpContext.Session.SetString("UserTemp", model.Email);
                        mBotAppVar.CurrentUser = model.Email;
                        wholeAccess = model.Email;
                        ////App variables

                        mBotAppVar.LoggedInCounter++;
                        mBotAppVar.IsItInUse = true;
                        ConstructorsAssigner(mBotAppVar);

                        _logger.LogInformation("User logged in.");
                        return RedirectToAction(nameof(RobotController.Index), "Robot");
                    }
                    else
                    {
                        //Log the main user out when 2nd user request
                        ViewBag.CountDown = 5;


                        mBotAppVar.testList.Add(model.Email.ToString());
                        mBotAppVar.users.Add(new LoginViewModel() { Email = model.Email });
                        HttpContext.Session.SetString("User", model.Email);

                        //App variables
                        mBotAppVar.LoggedInCounter++;
                        ConstructorsAssigner(mBotAppVar);


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
                        mBotAppVar.users.Add(new LoginViewModel() { Email = model.Email });
                        HttpContext.Session.SetString("User", model.Email);
                        mBotAppVar.CurrentUser = model.Email;
                        mBotAppVar.IsItInUse = true;
                        ConstructorsAssigner(mBotAppVar);
                        // TODO: id FSDAFDSA
                        mBotAppVar.LoggedInCounter++;
                        

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created a new account with password.");
                        return RedirectToAction(nameof(RobotController.Index), "Robot");
                    }
                    else
                    {
                        mBotAppVar.testList.Add(model.Email.ToString());
                        mBotAppVar.users.Add(new LoginViewModel() { Email = model.Email });
                        mBotAppVar.LoggedInCounter++;
                        ConstructorsAssigner(mBotAppVar);
                        HttpContext.Session.SetString("User", model.Email);
                        ModelState.AddModelError(string.Empty, "Someone has logged in");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created a new account with password.");
                        return RedirectToAction(nameof(HomeController.Start), "Home");
                    }
                  
                }
                AddErrors(result);
            }

            return View(model);
        }

        //Automatic logout.
        [HttpGet]
        public async Task<IActionResult> Logout(int? notUsedInt, string loggedOutEmail)
        {
            // LogoutHelper(loggedOutEmail);
            string test = loggedOutEmail;
            //if (mBotAppVar.LoggedInCounter > 0)
            //    mBotAppVar.LoggedInCounter--;
            //else
            //    mBotAppVar.LoggedInCounter = 0;

            LogoutHelper(loggedOutEmail);
            await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                return RedirectToAction(nameof(AccountController.Login));
            
            
            
        }

        //When logout button is pressed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string loggedOutEmail)
        {
             LogoutHelper(loggedOutEmail);

             await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AccountController.Login));
        }

        public void LogoutHelper(string loggedOutEmail)
        {
            if (loggedOutEmail == mBotAppVar.users.ElementAt(0).Email)
            {
                if (mBotAppVar.LoggedInCounter > 1)
                {
                    mBotAppVar.users.RemoveAt(0);
                    mBotAppVar.CurrentUser = mBotAppVar.users.ElementAt(0).Email;
                    mBotAppVar.LoggedInCounter--;
                    string oow = loggedOutEmail;
                    string hh = HttpContext.Session.GetString("UserTemp");

                }
                else if (mBotAppVar.LoggedInCounter == 1)
                {
                    mBotAppVar.users.RemoveAt(0);
                    mBotAppVar.IsItInUse = false;
                    mBotAppVar.LoggedInCounter = 0;
                }
                //Not sure about this case
                else
                {
                    mBotAppVar.LoggedInCounter = 0;
                }
            }
            else
            {
                int index = mBotAppVar.users.FindIndex(a => a.Email == loggedOutEmail);
                mBotAppVar.users.RemoveAt(index);
                mBotAppVar.LoggedInCounter--;
            }

            ConstructorsAssigner(mBotAppVar);
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

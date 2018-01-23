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
using MBotRangerCore.Services;
using MBotRangerCore.Helpers;

namespace MBotRangerCore.Controllers
{
    public class AccountController : Controller
    {

        public WaitingUsers waitListObj = new WaitingUsers();

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly MbotAppData mBotAppVar;


        public AccountController( UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  ILogger<AccountController> logger, 
								  IEmailSender emailSender,
								  MbotAppData _mBotAppVar )

        {
            mBotAppVar = _mBotAppVar;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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
            ViewBag.WaitList = mBotAppVar.users;

            //Check if the user Logged in
            if (!User.Identity.IsAuthenticated)  
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
            ViewBag.WaitList = mBotAppVar.users;
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
                        //If user already signed in, nothing will be done.
                        if (UserIsAlreadyLoggedIn(model.Email.ToString()))
                        {
                            
                        }
                        else
                        {
                            mBotAppVar.users.Add(new LoginViewModel() { Email = model.Email, LoggedInTime = DateTime.Now });
                            HttpContext.Session.SetString("User", model.Email);
                            mBotAppVar.CurrentUser = model.Email;
                            mBotAppVar.LoggedInCounter++;
                            mBotAppVar.IsItInUse = true;
                            waitListObj.usersLogOutTime.Add(model.Email, 10000);
                            waitListObj.usersTime.Add(model.Email, DateTime.Now);
                            ConstructorsAssigner(mBotAppVar);
                        }
                       
                        _logger.LogInformation("User logged in.");
                        return RedirectToAction(nameof(RobotController.Index), "Robot");
                    }
                    else
                    {
                        //Log the main user out when 2nd user request

                    //    mBotAppVar.TimerForLogout = 10000;
                        if (UserIsAlreadyLoggedIn(model.Email.ToString()))
                        {
                            ModelState.AddModelError(string.Empty, "Same user has logged in already.");
                     
                            // HttpContext.Session.SetString("User", model.Email);
                            // return RedirectToAction(nameof(RobotController.Index), "Robot");
                            return View();
                        }
                        else
                        {
                            
                            mBotAppVar.users.Add(new LoginViewModel() { Email = model.Email, LoggedInTime = DateTime.Now });
                            HttpContext.Session.SetString("User", model.Email);

                            //App variables
                            mBotAppVar.LoggedInCounter++;
                            ConstructorsAssigner(mBotAppVar);
                        }

                       
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

        private bool UserIsAlreadyLoggedIn (string newEmail)
        {
            bool userExist = false;

            foreach (var i in mBotAppVar.users)
            {
                if (newEmail.Equals(i.Email))
                    userExist = true;
            }
            return userExist;
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
                var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, DateOfBirth = model.DateOfBirth, Email = model.Email, UserName = model.Email ,EmailConfirmed=true};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!mBotAppVar.IsItInUse)
                    {
                        mBotAppVar.users.Add(new LoginViewModel() { Email = model.Email, LoggedInTime = DateTime.Now });
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
                        mBotAppVar.users.Add(new LoginViewModel() { Email = model.Email, LoggedInTime = DateTime.Now });
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

        public IActionResult LoseAccess(string loggedOutEmail)
        {
            LogoutHelper(loggedOutEmail);
            mBotAppVar.users.Add(new LoginViewModel() { Email = loggedOutEmail });
            return RedirectToAction(nameof(HomeController.Start), "Home");
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

                }
                else if (mBotAppVar.LoggedInCounter == 1)
                {
                    mBotAppVar.users.RemoveAt(0);
                    mBotAppVar.CurrentUser = "";
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
        
        // GET: Users/Forgot Password
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        
        // POST: Users/Forgot Password
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        // GET: Users/Forgot Password Confirm
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }



        // GET: Users/Reset Password
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View();
        }



        // POST: Users/Reset Password
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }



        // GET: Users/Reset Password Confirm Password
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
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

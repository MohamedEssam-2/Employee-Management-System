using Demo.PL.Utilities;
using Demo.PL.ViewModels.IdentityViewModels;
using Demo_DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var res = _userManager.CreateAsync(user, model.Password).Result;
            if (res.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                var IsCorrectPass = _userManager.CheckPasswordAsync(user, model.Password).Result;
                if (IsCorrectPass)
                {
                    var res = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                    if (res.IsNotAllowed) ModelState.AddModelError("", "Not Allowed to login");
                    if (res.IsLockedOut) ModelState.AddModelError("", "Your account is locked");


                    if (res.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login");
            }
            return View();
        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion



        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(model.Email).Result;

                if (user is not null)
                {
                    //Create Reset Password URL
                    //BaseUrl/Account/ResetPasswordLink?email
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetPasswordLink = Url.Action("ResetPasswordLink", "Account", new { email = model.Email, token }, Request.Scheme /*LocalHost & portNumber*/);
                    //Created Email
                    var email = new Email();
                    {
                        email.To = model.Email;
                        email.Subject = "Reset Password";
                        email.Body = resetPasswordLink;
                    }


                    //Send Email
                    var res = EmailSetting.SendEmail(email);
                    if (res) return RedirectToAction("CheckYourBox");


                }
            }
            ModelState.AddModelError("", "Invalid Email");
            return View("ForgetPassword", model);


        }


        #endregion

        public IActionResult CheckYourBox()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPasswordLink(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();

        }
        [HttpPost]
        public IActionResult ResetPasswordLink(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                  var email = TempData["email"] as string;
                  var token = TempData["token"] as string;
                  var user = _userManager.FindByEmailAsync(email).Result;
               
                if (user is not null)
                {
                    var res = _userManager.ResetPasswordAsync(user, token, model.Password).Result;
                    if (res.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in res.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    }
}
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WEA.Core.CommonServices;
using WEA.Core.Interfaces;
using WEA.Core.Services;
using WEA.Presentation.Areas.Administration.Controllers;
using WEA.Presentation.Helpers.Identity;
using WEA.Presentation.Models;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;
using CE = WEA.Core.Entities;
namespace WEA.Presentation.Controllers
{
    public class AccountController : BaseController
    {

        public const string Name = "Account";
        public const string Area = "";
        private readonly UserManager<CE.User> _userManager;
        private readonly IEmailService _emailService;
        private readonly CurrentUser _currentUser;
        private readonly UserService _userService;
        private readonly SignInManager<CE.User> _signInManager;
       // private readonly IAuthorizationService _authorizationService;
        public AccountController(UserManager<CE.User> userManager,
                                    SignInManager<CE.User> signInManager,
                                      //  IAuthorizationService authorizationService,
                                        IEmailService emailService,
                                        CurrentUser currentUser,
                                        UserService userService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _currentUser = currentUser;
            _userService = userService;
            _signInManager = signInManager;
          //  _authorizationService = authorizationService;
        }
        [HttpGet]
        public IActionResult RequireConfirmedEmail()
        {
            return Content("Require you email");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EmailConfirmation(string code, Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var decodedBytes = WebEncoders.Base64UrlDecode(code);
            var result = await _userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(decodedBytes));
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(SignIn));
            }
            return View("Error",new ErrorPageModel() { StatusCode = 500 , Message  = ExceptionMessages.EmailConfirmationEx});
        }
        
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token , Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var tokenString = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var model = new ResetPasswordModel()
            {
                Token = token,
                UserName = user.UserName
            };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ex", ExceptionMessages.PasswordAndConfirmPasswordNotSame);
                return View(model);
            }
            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    var messageResult = _userService.GetErrorMessageByErrorCode(error.Code);
                    if (messageResult.IsSucceed)
                        ModelState.AddModelError("ex", messageResult.Data);
                    else
                        ModelState.AddModelError("ex", error.Description);

                }
                
                return View(model);
            }

            return RedirectToAction(nameof(SignIn));
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotePassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotePassword(ForgetPasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await _userManager.FindByNameAsync(model.UserName);
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("ex", ExceptionMessages.EmailNotConfirmed);
                    return View(model);
                }

                var passwordRecoveryToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var base64String = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(passwordRecoveryToken));
                var resetLink = Url.Action(nameof(ResetPassword), Name, new { token = passwordRecoveryToken, userId = user.Id }, protocol: HttpContext.Request.Scheme);
                await _emailService.SendEmailAsync(user.Email, "WEA şifrəni təsdiqlə",
                $"Şifrəni yeniləmək üçün <a href='{HtmlEncoder.Default.Encode(resetLink)}'>bura kliklə</a>.");

                return RedirectToAction(nameof(SignIn));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ex", e.Message);
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model,string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("ex", ExceptionMessages.UserNotFound);
                    return View(model);
                }
                if (user.IsBlocked)
                {
                     ModelState.AddModelError("ex", ExceptionMessages.UserIsBlocked);
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction(nameof(UserController.Index),UserController.Name,new { area= UserController.Area });
               
                if (result.IsLockedOut)
                    ModelState.AddModelError("ex", ExceptionMessages.UserIsLockedOut);
                else if (result.IsNotAllowed)
                    ModelState.AddModelError("ex", ExceptionMessages.UserIsNotAllowed);
                else
                    ModelState.AddModelError("ex", ExceptionMessages.UserCredentialsIncorrect);
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ex", e.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult UserProfile()
        {
            return View("UserProfileOverview");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            return View("ChangePassword", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return AjaxFailureResult(Result.Failure(ExceptionMessages.UserNotFound));
            if (model.NewPasword != model.ConfirmPassword)
                return AjaxFailureResult(Result.Failure(ExceptionMessages.PasswordAndConfirmPasswordNotSame));

            var result = await  _userManager.ChangePasswordAsync(user,model.CurrentPassword,model.NewPasword);
            if (!result.Succeeded)
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    var messageResult = _userService.GetErrorMessageByErrorCode(error.Code);
                    if (messageResult.IsSucceed)
                        errors.Append(messageResult.Data + ", ");
                    else
                        errors.Append(error.Description + ", ");
                }
                return AjaxFailureResult(Result.Failure(errors.ToString().Trim().Substring(0,errors.Length - 2)));
            }
            return Json("Ok");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WEA.Core.Interfaces;
using WEA.Web.Attributes;
using WEA.Web.Helpers.Identity;
using CE = WEA.Core.Entities;
namespace WEA.Web.Controllers
{
    [PaymentExpiredAuthAttribute]
    public class AccountController : Controller
    {
        private readonly UserManager<CE.User> _userManager;
        private readonly IEmailService _emailService;
        private readonly CurrentUser _currentUser;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<CE.User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        public AccountController(UserManager<CE.User> userManager ,
                                    SignInManager<CE.User> signInManager,
                                        IAuthorizationService authorizationService,
                                        IEmailService emailService,
                                        CurrentUser currentUser,
                                        IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _currentUser = currentUser;
            _configuration = configuration;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var claims = await _userManager.GetClaimsAsync(user);
            var isSuperAdmin = _currentUser.IsSuperAdmin;

            //var data = await _authorizationService.AuthorizeAsync(User,"IsAdmin");
            //await _emailService.SendEmailAsync("rajab.khara@gmail.com","Warehouse","Hi",null);
            return Content("Congratulations");
        }
        [Authorize]
        public IActionResult PayedPage()
        {
            return Content("PayedPage");
        }
        [Authorize]
        public async Task<IActionResult> Refresh()
        {
            var user = await _userManager.FindByEmailAsync("rajab.khara@gmail.com");
            await _signInManager.RefreshSignInAsync(user);
            //var data = await _authorizationService.AuthorizeAsync(User,"IsAdmin");
            //await _emailService.SendEmailAsync("rajab.khara@gmail.com","Warehouse","Hi",null);
            return Content("Congratulations");
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Content("Logout");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp()
        {
            CE.User user = new CE.User()
            {
                Email = "rajab.khara@gmail.com",
                PhoneNumber = "+994553246307",
                UserName = "rajab.khara"
            };
            var result = await _userManager.CreateAsync(user, "Admin1234");
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var base64String = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callBackUrl = Url.Action(nameof(EmailConfirmation), "Account", new { code = base64String, userId = user.Id }, Request.Scheme);
                await _emailService.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>clicking here</a>.");

                if (_userManager.Options.SignIn.RequireConfirmedEmail)
                {
                    return RedirectToAction(nameof(RequireConfirmedEmail));
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index));
                }
            }
            return Content("Error");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn()
        
        {
            try
            {
                var user = await _userManager.FindByNameAsync("admin");
                if (user == null)
                {
                    return RedirectToAction(nameof(SignUp));
                }
                if (user.IsBlocked)
                {
                    return Content("Hesabiniz bloklanib");
                }
                var result = await _signInManager.PasswordSignInAsync(user, _configuration["SeedAdminPW"], true,false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(SignUp));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        public IActionResult RequireConfirmedEmail()
        {
            return Content("Requre confirmation");
        }
        [HttpGet]
        public async Task<IActionResult> EmailConfirmation(string code , Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var decodedBytes = WebEncoders.Base64UrlDecode(code);
            var result = await  _userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(decodedBytes));
            if (result.Succeeded)
            {
               return RedirectToAction(nameof(SignIn));
            }
            return Content("Error happend");
        }
    }
}

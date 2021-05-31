using AutoMapper;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Transactions;
using WEA.Core.Entities;
using WEA.Core.Interfaces;
using WEA.Core.Interfaces.Services;
using WEA.Core.Models;
using WEA.Presentation.Areas.Administration.Models;
using WEA.Presentation.Areas.Administration.Services;
using WEA.Presentation.Controllers;
using WEA.Presentation.Helpers.Statics;
using WEA.Presentation.Services;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;

namespace WEA.Presentation.Areas.Administration.Controllers
{
    [Area(AreaConstants.Admin)]
    public class UserController : BaseController
    {
        public const string Name = "User";
        public const string Area = AreaConstants.Admin;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserServiceFacade _facade;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly SignInManager<User> _signInManager;
        private readonly AccountServiceFacade _accountServiceFacade;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAttachmentService _attachmentService;
        private readonly FileServiceFacade _fileService;

        public UserController(UserManager<User> userManager,
            RoleManager<Role> roleManager,
                            IUnitOfWork unitOfWork,
                                UserServiceFacade facade,
                                IMapper mapper,
                                IEmailService emailService,
                                SignInManager<User> signInManager,
                                AccountServiceFacade accountServiceFacade,
                                IConfiguration configuration,
                                IWebHostEnvironment webHostEnvironment,
                                IAttachmentService attachmentService,
                                FileServiceFacade fileService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _facade = facade;
            _mapper = mapper;
            _emailService = emailService;
            _signInManager = signInManager;
            _accountServiceFacade = accountServiceFacade;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _attachmentService = attachmentService;
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetData(DataSourceLoadOptions options)
        {
            var result = _facade.GetData();
            if (result.IsSucceed)
                return Load(result.Data, options);
            return AjaxFailureResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> Add(Guid? id)
        {
            var model = new UserViewModel();
            if (id.HasValue)
            {
                var result = await _facade.GetModel(id.Value);
                if (result.IsSucceed)
                    model = result.Data;
                else
                    return AjaxFailureResult(result);
            }
            return View("Form", model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                var user = _mapper.Map<User>(model);
                var passwordResult = _accountServiceFacade.GenerateRandomPassword();
                if (!passwordResult.IsSucceed)
                {
                    return AjaxFailureResult(passwordResult);
                }
               
                using (var scope = _unitOfWork.CreateScoppedTransaction())
                {
                    var result = await _userManager.CreateAsync(user, passwordResult.Data);
                    if (result.Succeeded)
                    {
                        StringBuilder roles = new StringBuilder();
                        foreach (var roleId in model.Roles[0].Split(','))
                        {
                            var role = await _roleManager.FindByIdAsync(roleId);
                            if (role.IsSuperAdmin)
                                user.IsAdmin = true;

                            var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
                            if (!roleResult.Succeeded)
                                return AjaxFailureResult(SharedKernel.Result.Failure($"{role.Name} adlı rol əlavə oluna bilmədi"));

                            roles.Append(role.Name + ", ");
                        }
                        user.DN_RoleNames = roles.ToString().Trim().Substring(0, roles.Length - 2);
                        var updateResult = await _userManager.UpdateAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var base64String = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callBackUrl = Url.Action(nameof(AccountController.EmailConfirmation), AccountController.Name, new { area = AccountController.Area, code = base64String, userId = user.Id }, Request.Scheme);
                        await _emailService.SendEmailAsync(user.Email, "WEA email təsdiqlə",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>clicking here</a>. Your password is {passwordResult.Data}.");
                        var fileResult = await _fileService.UploadAsync(new Presentation.Models.AttachmentViewModel()
                        {
                            ProductId = user.Id,
                            Base64String = model.Base64String,
                            ContentType = model.ContentType,
                            FileName = model.FileName
                        });
                        if (fileResult.IsSucceed)
                            await _unitOfWork.CommitAsync();
                        else
                            return AjaxFailureResult(fileResult);
                        /*if (_userManager.Options.SignIn.RequireConfirmedEmail)
                        {
                            return RedirectToAction(nameof(AccountController.RequireConfirmedEmail), AccountController.Name, new { area = AccountController.Area });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction(nameof(Index));
                        }*/
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else
            {
                var exUser  = await _userManager.FindByIdAsync(model.Id.ToString());
                string previouseRoles = exUser.Roles;
                var updatedValues = _mapper.Map(model,exUser);
                if (previouseRoles != updatedValues.Roles)
                {
                    foreach (var roleName in await _userManager.GetRolesAsync(exUser))
                    {
                        var roleResult = await _userManager.RemoveFromRoleAsync(exUser, roleName);
                        if (!roleResult.Succeeded)
                            return AjaxFailureResult(SharedKernel.Result.Failure($"{roleName} adlı rol silinə bilmədi"));
                    }
                    StringBuilder roles = new StringBuilder();
                    updatedValues.IsAdmin = false;
                    foreach (var roleId in model.Roles[0].Split(','))
                    {
                        var role = await _roleManager.FindByIdAsync(roleId);
                        if (role.IsSuperAdmin)
                            updatedValues.IsAdmin = true;
                        var roleResult = await _userManager.AddToRoleAsync(updatedValues, role.Name);
                        if (!roleResult.Succeeded)
                            return AjaxFailureResult(SharedKernel.Result.Failure($"{role.Name} adlı rol əlavə oluna bilmədi"));

                        roles.Append(role.Name + ", ");
                    }
                    updatedValues.DN_RoleNames = roles.ToString().Trim().Substring(0, roles.Length - 2);
                }
                var result = await _userManager.UpdateAsync(updatedValues);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
          
            return Content("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _facade.Remove(id);
            if (result.IsSucceed)
                return Json("Ok");
            return AjaxFailureResult(result);
        }
    }
}

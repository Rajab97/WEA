using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using WEA.Core.Entities;
using WEA.Core.Events;
using WEA.Core.Interfaces;
using WEA.Core.Interfaces.Services;
using WEA.Core.Repositories;
using WEA.SharedKernel.Interfaces;
using WEA.Web.Attributes;
using WEA.Web.Helpers.Identity.Authorization.Requirements;
using WEA.Web.Models;

namespace WEA.Web.Controllers
{
   
    [PaymentExpiredAuthAttribute]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IOrganizationRepository organizationRepository;

        private readonly IOrganizationService _dataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public HomeController(ILogger<HomeController> logger ,
                                IOrganizationService dataService,
                                    IUnitOfWork unitOfWork,
                                        UserManager<User> userManager,
                                            IAuthorizationService  authorizationService)
        {
            _logger = logger;
            _dataService = dataService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
           /* using (_unitOfWork.CreateScoppedTransaction())
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var authResult = await _authorizationService.AuthorizeAsync(User,null, ConstantOperationRequirements.Create);
                    if (authResult.Succeeded)
                    {
                        var organization = new Organization()
                        {
                            OrganizationName = "QQ Holding",
                            OwnerId = user.Id,
                            ExpiredDate = DateTime.Today.AddDays(-1)
                        };
                        await _dataService.CreateAsync(organization);
                        await _unitOfWork.CommitAsync();
                    }
                }
                //await _unitOfWork.CommitAsync();
            }*/

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error(string code)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

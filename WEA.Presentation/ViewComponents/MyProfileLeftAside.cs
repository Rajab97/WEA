using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Presentation.Models;

namespace WEA.Presentation.ViewComponents
{
    public class MyProfileLeftAside : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;

        public MyProfileLeftAside(IHttpContextAccessor httpContext , UserManager<User> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

            if (user == null)
            {
                return View(new MyProfileAsideModel());
            }
            var model = new MyProfileAsideModel() {
                FullName = user.FirstName +" " + user.LastName,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }
    }
}

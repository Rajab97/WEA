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
    public class QuickUser : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuickUser(UserManager<User> userManager,IHttpContextAccessor httpContextAccessor)
        {
            this._userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new QuickUserModel();
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            model.User = user;
            model.Roles = await _userManager.GetRolesAsync(user);
            return View(model);
        }
    }
}

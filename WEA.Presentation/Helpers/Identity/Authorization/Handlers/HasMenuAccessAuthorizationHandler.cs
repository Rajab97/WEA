using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Interfaces.Services;
using WEA.Presentation.Helpers.Identity.Authorization.Requirements;

namespace WEA.Presentation.Helpers.Identity.Authorization.Handlers
{
    public class HasMenuAccessAuthorizationHandler : AuthorizationHandler<HasMenuAccessRequirement>
    {
        private readonly IMenuService _menuService;
        private readonly UserManager<User> _userManager;
        private readonly IRoleMenuService _roleMenuService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HasMenuAccessAuthorizationHandler(IMenuService menuService,
                                                    UserManager<User> userManager,
                                                        IRoleMenuService roleMenuService,
                                                        IHttpContextAccessor httpContextAccessor) 
        {
            _menuService = menuService;
            _userManager = userManager;
            _roleMenuService = roleMenuService;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HasMenuAccessRequirement requirement)
        {
            var area = _httpContextAccessor.HttpContext.Request.RouteValues.FirstOrDefault(m => m.Key == "area").Value?.ToString();
            var controller = _httpContextAccessor.HttpContext.Request.RouteValues.FirstOrDefault(m => m.Key == "controller").Value?.ToString();
            var action = _httpContextAccessor.HttpContext.Request.RouteValues.FirstOrDefault(m => m.Key == "action").Value?.ToString();
            var menuRes = _menuService.GetMenuByRouteDetails(area,controller,action);
            if (!menuRes.IsSucceed)
            {
                context.Succeed(requirement);
                return;
            }
            var user = await _userManager.GetUserAsync(context.User);
            if (user == null)
            {
                return;
            }
            var roles = await _userManager.GetRolesAsync(user);

            var result = _roleMenuService.HasAccessToMenu(menuRes.Data.Id,roles);
            if (result.IsSucceed)
                context.Succeed(requirement);
        }
    }
}

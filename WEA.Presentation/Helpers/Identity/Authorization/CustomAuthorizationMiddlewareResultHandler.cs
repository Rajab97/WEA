using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WEA.Core.Entities;
using WEA.Presentation.Helpers.Identity.Authorization.Requirements;
using STA = WEA.Presentation.Helpers.Statics;
namespace WEA.Presentation.Helpers.Identity.Authorization
{
    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly IAuthorizationMiddlewareResultHandler _handler;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CustomAuthorizationMiddlewareResultHandler(UserManager<User> userManager , SignInManager<User> signInManager)
        {
            _handler = new AuthorizationMiddlewareResultHandler();
            _userManager = userManager;
            _signInManager = signInManager;
            _signInManager = signInManager;
        }
        public async Task HandleAsync(RequestDelegate next,
                                    HttpContext context, 
                                        AuthorizationPolicy policy,
                                            PolicyAuthorizationResult authorizeResult)
        {
            
            if (authorizeResult.Forbidden && authorizeResult.AuthorizationFailure != null)
            {
                if (authorizeResult.AuthorizationFailure.FailedRequirements.Any(req => req is ExpiredPaymentRequirement))
                {
                    var user = await _userManager.GetUserAsync(context.User);
                    user.IsBlocked = true;
                    await _userManager.UpdateAsync(user);
                    await _signInManager.SignOutAsync();
                }
            }
            await _handler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}

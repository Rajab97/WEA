using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Helpers.Constants;

namespace WEA.Web.Helpers.Identity.Authorization.Handlers
{
    public class AdminAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            if (context.User.IsInRole(RolesConstants.Admin))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Web.Helpers.Identity.Authorization.Requirements;

namespace WEA.Web.Helpers.Identity.Authorization.Handlers
{
    public class ExpiredPaymentAuthorizationHandler : AuthorizationHandler<ExpiredPaymentRequirement>
    {
        private readonly CurrentUser _currentUser;

        public ExpiredPaymentAuthorizationHandler(CurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExpiredPaymentRequirement requirement)
        {
            DateTime? paymentExDate = _currentUser.PaymentExDate;
            if (paymentExDate.HasValue && DateTime.Compare(paymentExDate.Value, DateTime.Today) >= 0)
            {
                context.Succeed(requirement);
            }
            else if(!paymentExDate.HasValue)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

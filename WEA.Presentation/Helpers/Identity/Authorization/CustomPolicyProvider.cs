using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Presentation.Helpers.Identity.Authorization.Requirements;

namespace WEA.Presentation.Helpers.Identity.Authorization
{
    public class CustomPolicyProvider : IAuthorizationPolicyProvider
    {
        public const string PAYMENT_EXPIRED_POLICY = "PaymentExpired";
        public const string MENU_ACCESS_POLICY = "MenuAccess";
        public DefaultAuthorizationPolicyProvider DefaultAuthorizationPolicyProvider { get; }
        public CustomPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            DefaultAuthorizationPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return DefaultAuthorizationPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return DefaultAuthorizationPolicyProvider.GetFallbackPolicyAsync();
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(PAYMENT_EXPIRED_POLICY, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new ExpiredPaymentRequirement());
                return Task.FromResult(policy.Build());
            }
            if (policyName.StartsWith(MENU_ACCESS_POLICY,StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new HasMenuAccessRequirement());
                return Task.FromResult(policy.Build());
            }
            return DefaultAuthorizationPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}

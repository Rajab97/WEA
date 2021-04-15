using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Web.Helpers.Identity.Authorization.Requirements;

namespace WEA.Web.Helpers.Identity.Authorization
{
    public class CustomPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "PaymentExpired";
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
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new ExpiredPaymentRequirement());
                return Task.FromResult(policy.Build());
            }
            return DefaultAuthorizationPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}

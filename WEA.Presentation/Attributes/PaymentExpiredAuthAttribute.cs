using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Presentation.Helpers.Identity.Authorization;

namespace WEA.Presentation.Attributes
{
    public class PaymentExpiredAuthAttribute : AuthorizeAttribute
    {

        public PaymentExpiredAuthAttribute()
        {
            Policy = CustomPolicyProvider.PAYMENT_EXPIRED_POLICY;
        }
    }
}

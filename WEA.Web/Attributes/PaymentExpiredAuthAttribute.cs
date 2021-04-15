using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEA.Web.Attributes
{
    public class PaymentExpiredAuthAttribute : AuthorizeAttribute
    {

        public PaymentExpiredAuthAttribute()
        {
            Policy = "PaymentExpired";
        }
    }
}

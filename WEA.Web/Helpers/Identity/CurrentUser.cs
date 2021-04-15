using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEA.Web.Helpers.Identity
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsSuperAdmin { get { return _httpContextAccessor.HttpContext.User.Identity.IsSuperAdmin(); } }
        public bool IsOwner { get { return _httpContextAccessor.HttpContext.User.Identity.IsOwner(); } }
        public DateTime? PaymentExDate { get { return _httpContextAccessor.HttpContext.User.Identity.PaymentExpiredDate(); } }
    }
}

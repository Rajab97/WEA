using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;

namespace WEA.Presentation.Helpers.Identity
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
        public IEnumerable<Menu> Menus { get { return _httpContextAccessor.HttpContext.User.Identity.Menus(); } }
        public DateTime? PaymentExDate { get { return _httpContextAccessor.HttpContext.User.Identity.PaymentExpiredDate(); } }
    }
}

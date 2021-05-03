using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Presentation.Helpers.Identity.Authorization;

namespace WEA.Presentation.Attributes
{
    public class HasMenuAccessAttribute : AuthorizeAttribute
    {

        public HasMenuAccessAttribute()
        {
            Policy = CustomPolicyProvider.MENU_ACCESS_POLICY;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public Organization Organization { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
    }
}

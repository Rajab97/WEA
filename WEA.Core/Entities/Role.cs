using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.Core.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public bool IsSuperAdmin { get; set; }
        public bool IsDefaultRole { get; set; }
        public ICollection<RoleMenu> RoleMenus { get; set; }
    }
}

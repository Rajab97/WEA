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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBith { get; set; }
        public string Address { get; set; }
        public string WorkNumber { get; set; }
        public string Roles { get; set; }

        public string DN_RoleNames { get; set; }

    }
}

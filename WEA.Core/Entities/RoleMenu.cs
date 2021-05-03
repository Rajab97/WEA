using System;
using System.Collections.Generic;
using System.Text;
using WEA.SharedKernel;

namespace WEA.Core.Entities
{
    public class RoleMenu : BaseEntity
    {
        public Guid MenuID { get; set; }
        public Menu Menu { get; set; }

        public Guid RoleID { get; set; }
        public Role Role { get; set; }

        public bool CanEdit { get; set; }
        public bool CanCreate { get; set; }
        public bool CanDelete { get; set; }
    }
}

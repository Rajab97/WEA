using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.Core.ViewEntities
{
    public class RoleMenuGridView
    {
        public Guid Id { get; set; }
        public Guid MenuID { get; set; }
        public string RoleName { get; set; }

        public Guid RoleID { get; set; }
        public string MenuName { get; set; }

        public bool CanEdit { get; set; }
        public bool CanCreate { get; set; }
        public bool CanDelete { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;

namespace WEA.Presentation.Models
{
    public class SubMenuComponentModel
    {
        public IEnumerable<Menu> Menus { get; set; }
        public Guid ParentId { get; set; }
    }
}

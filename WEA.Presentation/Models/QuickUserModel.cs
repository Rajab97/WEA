using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;

namespace WEA.Presentation.Models
{
    public class QuickUserModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}

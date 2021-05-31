using System;
using System.Collections.Generic;
using System.Text;
using WEA.SharedKernel;

namespace WEA.Core.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

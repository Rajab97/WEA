using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;

namespace WEA.Core.Specification
{
    class IncompletedItemSpecification : Specification<Organization>
    {
        public IncompletedItemSpecification()
        {
            //Query.Where(item => !item.IsDone).Skip(1);
        }
    }
}

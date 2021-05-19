using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.SharedKernel
{
    public abstract class DeleteEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateOfDelete { get; set; }
    }
}

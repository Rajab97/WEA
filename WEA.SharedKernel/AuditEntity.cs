using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.SharedKernel
{
    public abstract class AuditEntity : DeleteEntity
    {
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
}

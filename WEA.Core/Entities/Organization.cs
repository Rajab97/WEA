using System;
using System.Collections.Generic;
using System.Text;
using WEA.SharedKernel;

namespace WEA.Core.Entities
{
    public class Organization : AuditEntity
    {
        public string OrganizationName { get; set; }
        public DateTime? ExpiredDate { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}

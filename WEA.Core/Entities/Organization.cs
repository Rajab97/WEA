using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Helpers.Enums;
using WEA.SharedKernel;

namespace WEA.Core.Entities
{
    public class Organization : AuditEntity
    {
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}

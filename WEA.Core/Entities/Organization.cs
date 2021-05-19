using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Helpers.Enums;
using WEA.SharedKernel;

namespace WEA.Core.Entities
{
    public class Organization : AuditEntity
    {
        public string IdentificationNumber { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public decimal? Lattitude { get; set; }
        public decimal? Longtitude { get; set; }

        public string ProductType { get; set; }
        public DateTime? ExpiredDate { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}

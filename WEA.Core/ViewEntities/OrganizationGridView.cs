using System;
using System.Collections.Generic;
using System.Text;
using WEA.SharedKernel;

namespace WEA.Core.ViewEntities
{
    public class OrganizationGridView
    {
        public Guid Id { get; set; }

        public int Version { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateOfDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUserId { get; set; }
        public string IdentificationNumber { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public decimal? Lattitude { get; set; }
        public decimal? Longtitude { get; set; }

        public string ProductType { get; set; }
        public DateTime? ExpiredDate { get; set; }

        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public string OwnerUserName { get; set; }

    }
}

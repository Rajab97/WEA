using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;

namespace WEA.Infrastructure.Data.Config
{
    internal class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasOne(m => m.Owner).WithOne(m => m.Organization).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

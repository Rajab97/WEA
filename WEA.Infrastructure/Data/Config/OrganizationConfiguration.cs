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
            builder.Property(m => m.OrganizationAddress).HasMaxLength(250);
            builder.Property(m => m.OrganizationName).HasMaxLength(250).IsRequired(true);
            builder.Property(m => m.ProductType).HasMaxLength(50).IsRequired(true);
            builder.Property(m => m.TelephoneNumber).HasMaxLength(25);
            builder.Property(m => m.IdentificationNumber).HasMaxLength(25).IsRequired(true);
            builder.HasIndex(m => m.IdentificationNumber).IsUnique(true);
        }
    }
}

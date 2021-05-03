using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;

namespace WEA.Infrastructure.Data.Config
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(m => m.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(m => m.LastName).IsRequired(true).HasMaxLength(50);
            builder.Property(m => m.Patronymic).HasMaxLength(50);
            builder.Property(m => m.PhoneNumber).IsRequired(true).HasMaxLength(50);
            builder.Property(m => m.WorkNumber).IsRequired(false).HasMaxLength(50);
            builder.Property(m => m.Email).IsRequired(true).HasMaxLength(50);
            builder.Property(m => m.Address).HasMaxLength(500);
            builder.Property(m => m.DateOfBith).IsRequired(true).HasColumnType("date");
        }
    }
}

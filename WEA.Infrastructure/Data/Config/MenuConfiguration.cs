using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;

namespace WEA.Infrastructure.Data.Config
{
    class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            // builder.HasOne(m => m.Parent).WithOne(m => m).OnDelete(DeleteBehavior.Restrict);
            builder.Property(m => m.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(m => m.Title).HasMaxLength(100).IsRequired(true);
            builder.Property(m => m.Url).HasMaxLength(500);
            builder.Property(m => m.CssClass).HasMaxLength(200);
            builder.Property(m => m.Icon).HasMaxLength(100);
            builder.Property(m => m.Action).HasMaxLength(200).IsRequired(true);
            builder.Property(m => m.Area).HasMaxLength(100);
            builder.Property(m => m.Controller).HasMaxLength(100).IsRequired(true);
            builder.HasIndex(m => new { m.Area, m.Controller, m.Action });
        }
    }
}

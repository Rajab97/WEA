using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;

namespace WEA.Infrastructure.Data.Config
{
    public class CarModelConfiguration : IEntityTypeConfiguration<CarModel>
    {
        public void Configure(EntityTypeBuilder<CarModel> builder)
        {
            builder.Property(m => m.Name).HasMaxLength(250);
            builder.Property(m => m.Code).HasMaxLength(25);
            builder.Property(m => m.Description).HasMaxLength(500);
            builder.HasOne(m => m.CarBrand).WithMany(m => m.CarModels).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(m => m.Code).IsUnique(true);
            builder.HasIndex(m => m.Name).IsUnique(true);
        }
    }
}

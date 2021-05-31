using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;

namespace WEA.Infrastructure.Data.Config
{
    class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.Property(m => m.ContentType).HasMaxLength(50).IsRequired(true);
            builder.Property(m => m.FileExtension).HasMaxLength(50).IsRequired(true);
            builder.Property(m => m.FileName).HasMaxLength(250).IsRequired(true);
            builder.Property(m => m.FilePath).HasMaxLength(250).IsRequired(true);
            builder.Property(m => m.Description).HasMaxLength(250);
            builder.HasIndex(m => m.ProductId);
        }
    }
}

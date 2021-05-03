using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;

namespace WEA.Infrastructure.Data.Config
{
    class RoleMenuConfiguration : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            builder.HasIndex(m => new { m.MenuID, m.RoleID }).IsUnique(true);
            builder.HasOne(m => m.Menu).WithMany(m => m.RoleMenus).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.Role).WithMany(m => m.RoleMenus).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

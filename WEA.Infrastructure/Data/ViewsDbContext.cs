using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.ViewEntities;

namespace WEA.Infrastructure.Data
{
    public class ViewsDbContext : DbContext
    {
        public ViewsDbContext(DbContextOptions<ViewsDbContext> options)
           : base(options)
        {

        }
        public DbSet<RoleMenuGridView> RoleMenuGridView { get; set; }
        public DbSet<UserMenusEntityView> UserMenusView { get; set; }
        public DbSet<OrganizationGridView> OrganizationGridView { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoleMenuGridView>().ToTable("RoleMenuGridView");
            modelBuilder.Entity<UserMenusEntityView>().ToTable("UserMenus");
            modelBuilder.Entity<OrganizationGridView>().ToTable("OrganizationGridView");
        }
    }
}

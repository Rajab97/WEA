using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Helpers.Constants;
using WEA.Infrastructure.Data;

namespace WEA.Web
{
    public static class SeedData
    {
      /*  private static IEnumerable<TestEntity> GetTestItems()
        {
            yield return new TestEntity
            {
                Title = "Get Sample Working",
                Description = "Try to get the sample to build."
            };
            yield return new TestEntity
            {
                Title = "Review Solution",
                Description = "Review the different projects in the solution and how they relate to one another."
            };
            yield return new TestEntity
            {
                Title = "Run and Review Tests",
                Description = "Make sure all the tests run and review what they are doing."
            };
        }*/

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
            {
                // Look for any TODO items.
                /*  if (dbContext.TestEntities.Any())
                  {
                      return;   // DB has been seeded
                  }
  */
                await AddUsers(serviceProvider);
                await AddRoles(serviceProvider);
                PopulateTestData(dbContext);


            }
        }
        public static async Task AddUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var adminUser = await userManager.FindByEmailAsync("rajab.khara@gmail.com");
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            if (adminUser == null)
            {
                adminUser = new User()
                {
                    Email = "rajab.khara@gmail.com",
                    UserName = "admin",
                    IsAdmin = true,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await userManager.CreateAsync(adminUser, configuration["SeedAdminPW"]);
            }
        }
        public static async Task AddRoles(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            if (!await roleManager.RoleExistsAsync(RolesConstants.Admin))
            {
                await roleManager.CreateAsync(new Role() { Name = RolesConstants.Admin });
            }
            if (!await roleManager.RoleExistsAsync(RolesConstants.User))
            {
                await roleManager.CreateAsync(new Role() { Name = RolesConstants.User });
            }

            var admin = await userManager.FindByNameAsync("admin");
            if (admin != null)
            {
               await userManager.AddToRoleAsync(admin,RolesConstants.Admin);
            }
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
         /*   foreach (var item in dbContext.TestEntities)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();

            foreach (var item in GetTestItems())
            {
                dbContext.TestEntities.Add(item);
            }*/

            dbContext.SaveChanges();
        }
    }
}

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
using WEA.Core.Interfaces.Services;
using WEA.Core.Services;
using WEA.Infrastructure.Data;
using WEA.SharedKernel.Interfaces;
using WEA.SharedKernel.Resources;

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
  */            await AddOrganization(serviceProvider);
                await AddRoles(serviceProvider);
                await AddUsers(serviceProvider);
                var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                await unitOfWork.CommitAsync();
                PopulateTestData(dbContext);


            }
        }

        private static async Task AddOrganization(IServiceProvider serviceProvider)
        {
            var organizationService = serviceProvider.GetRequiredService<IOrganizationService>();
            if (!organizationService.GetAll().Data.Any())
            {
                var defaulOrganization = new Organization()
                {
                    OrganizationAddress = "Default",
                    OrganizationName = "Default"
                };
                var result = await organizationService.CreateAsync(defaulOrganization);
                if (!result.IsSucceed)
                {
                    throw new Exception(ExceptionMessages.ObjectNotCreated);
                }
            }
        }

        public static async Task AddUsers(IServiceProvider serviceProvider)
        {
            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var superAdminUser = await userManager.FindByEmailAsync("rajab.khara@gmail.com");
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                if (superAdminUser == null)
                {
                    superAdminUser = new User()
                    {
                        FirstName = "Rəcəb",
                        LastName = "Qarayev",
                        Email = "rajab.khara@gmail.com",
                        PhoneNumber = "+(994)55-324-63-07",
                        UserName = "super_admin",
                        IsAdmin = true,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };
                    var result = await userManager.CreateAsync(superAdminUser, configuration["SeedAdminPW"]);
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(superAdminUser, RolesConstants.SuperAdmin);
                }
            }
            catch (Exception e)
            {
                throw new Exception(ExceptionMessages.UsersNotCreated);
            }
        }
        public static async Task AddRoles(IServiceProvider serviceProvider)
        {
            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                if (!await roleManager.RoleExistsAsync(RolesConstants.Customer))
                    await roleManager.CreateAsync(new Role() { Name = RolesConstants.Customer, IsDefaultRole = true });
                if (!await roleManager.RoleExistsAsync(RolesConstants.Admin))
                    await roleManager.CreateAsync(new Role() { Name = RolesConstants.Admin, IsDefaultRole = true });
                if (!await roleManager.RoleExistsAsync(RolesConstants.Owner))
                    await roleManager.CreateAsync(new Role() { Name = RolesConstants.Owner, IsDefaultRole = true });
                if (!await roleManager.RoleExistsAsync(RolesConstants.SuperAdmin))
                    await roleManager.CreateAsync(new Role() { Name = RolesConstants.SuperAdmin, IsDefaultRole = true, IsSuperAdmin = true });
            }
            catch (Exception e)
            {
                throw new Exception(ExceptionMessages.RolesNotCreated);
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

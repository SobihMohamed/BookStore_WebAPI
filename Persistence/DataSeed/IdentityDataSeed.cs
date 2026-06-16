using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistence.DataSeed
{
    public static class IdentityDataSeed
    {
        public static async Task SeedRolesAndAdminAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // 1. seeding roles
            foreach (var role in Enum.GetNames(typeof(AppRoles)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 2. seeding admin user
            var adminEmail = "admin@bookstore.com";
            if (userManager.Users.All(u => u.Email != adminEmail))
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    PhoneNumber = "01000000000"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@1234");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, AppRoles.Admin.ToString());
                }
            }
        }
    }
}
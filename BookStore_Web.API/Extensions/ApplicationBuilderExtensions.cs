using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.DataSeed;

namespace BookStore_Web.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    await IdentityDataSeed.SeedRolesAndAdminAsync(roleManager, userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger("DatabaseSeeder");
                    logger.LogError(ex, "An error occurred during seeding the Identity data.");
                }
            }
        }
    }
}
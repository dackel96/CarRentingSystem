using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static CarRentingSystem.WebConstants;
namespace CarRentingSystem.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedService = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedService.ServiceProvider;

            MigrateDatabase(serviceProvider);

            SeedCategories(serviceProvider);
            SeedAdministrator(serviceProvider);


            return app;
        }

        private static void MigrateDatabase(IServiceProvider service)
        {
            var data = service.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {

            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category{ Name = "Mini" },
                new Category{ Name = "Economy" },
                new Category{ Name = "Midsize" },
                new Category{ Name = "Large" },
                new Category{ Name = "SUV" },
                new Category{ Name = "Vans" },
                new Category{ Name = "Luxury" }
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdministratorRoleName };

                await roleManager.CreateAsync(role);

                var nameEmail = "admin@crs.com";
                var adminPass = "admin0000";

                var user = new User
                {
                    Email = nameEmail,
                    UserName = nameEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPass);

                await userManager.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}

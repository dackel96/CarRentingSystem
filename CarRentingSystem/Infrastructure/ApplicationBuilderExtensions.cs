 using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedService = app.ApplicationServices.CreateScope();

            var data = scopedService.ServiceProvider.GetService<ApplicationDbContext>();

            if (data != null)
            {
                data.Database.Migrate();

                SeedCategories(data);
            }

            return app;
        }

        private static void SeedCategories(ApplicationDbContext data)
        {
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
    }
}

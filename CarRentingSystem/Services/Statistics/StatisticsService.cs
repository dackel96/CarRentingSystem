using CarRentingSystem.Data;

namespace CarRentingSystem.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext data;

        public StatisticsService(ApplicationDbContext data)
            => this.data = data;


        public StatisticsServiceModel Total()
        {
            var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers,
            };
        }
    }
}

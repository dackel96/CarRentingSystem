using CarRentingSystem.Data;

namespace CarRentingSystem.Services.Dealers
{
    public class DealerService : IDealerService
    {
        private readonly ApplicationDbContext data;

        public DealerService(ApplicationDbContext data)
            => this.data = data;

        public int GetIdByUser(string userId)
         => this.data
                .Dealers
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .FirstOrDefault();

        public bool IsDealer(string userId)
            => this.data.Dealers.Any(x => x.UserId == userId);
    }
}

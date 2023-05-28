namespace CarRentingSystem.Services.Cars.Models
{
    public class CarDetailsServiceModel : CarServiceModel
    {
        public int DealerId { get; init; }

        public string DealerName { get; init; } = null!;

        public string UserId { get; init; } = null!;

        public int CategoryId { get; init; }
    }
}

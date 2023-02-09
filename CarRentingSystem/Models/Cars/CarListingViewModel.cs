namespace CarRentingSystem.Models.Cars
{
    public class CarListingViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int Year { get; set; }

        public string Category { get; set; } = null!;
    }
}

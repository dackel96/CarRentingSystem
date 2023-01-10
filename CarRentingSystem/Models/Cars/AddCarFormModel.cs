using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Models.Cars
{
    public class AddCarFormModel
    {

        public string Brand { get; init; } = null!;

        public string Model { get; init; } = null!;


        public string Description { get; init; } = null!;


        public string ImageUrl { get; init; } = null!;

        public int Year { get; init; }

        public int CategoryId { get; init; }
    }
}

// Бележка!!!
// Използваме init тук тъй като това е само трансферен модел няма за цел да редактира данни а да ги прекарва
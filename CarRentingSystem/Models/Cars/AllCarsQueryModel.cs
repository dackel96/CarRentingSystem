using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Models.Cars
{
    public class AllCarsQueryModel
    {
        public const int CarsPerPage = 2;

        public int CurrentPage { get; init; } = 1;

        public CarSorting Sorting { get; init; }
        [Display(Name = "Search")]
        public string SearchTerm { get; init; } = null!;

        public string Brand { get; init; } = null!;

        public int TotalCars { get; set; }

        public IEnumerable<string> Brands { get; set; } = new List<string>();

        public IEnumerable<CarListingViewModel> Cars { get; set; } = new List<CarListingViewModel>();
    }
}

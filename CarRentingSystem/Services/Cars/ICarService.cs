using CarRentingSystem.Data.Models;
using CarRentingSystem.Models;
using CarRentingSystem.Services.Cars.Models;

namespace CarRentingSystem.Services.Cars
{
    public interface ICarService
    {
        CarQueryServiceModel All(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage);

        IEnumerable<string> AllCarBrands();

        IEnumerable<CarServiceModel> ByUser(string userId);

        IEnumerable<CarCategoryServiceModel> AllCarCategories();

        CarDetailsServiceModel Details(int carId);

        bool CategoryExists(int categoryId);

        bool IsByDealer(int carId, int dealerId);

        int Create(string brand,
                   string model,
                   string description,
                   string imageUrl,
                   int year,
                   int categoryId,
                   int dealerId);
        bool Edit(
                  int carId,
                  string brand,
                  string model,
                  string description,
                  string imageUrl,
                  int year,
                  int categoryId);
    }
}

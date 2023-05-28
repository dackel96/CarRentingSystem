using CarRentingSystem.Models;
using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;
using CarRentingSystem.Services.Cars.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace CarRentingSystem.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CarService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public CarQueryServiceModel All(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(x => x.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                carsQuery = carsQuery.Where(x =>
                    (x.Brand + " " + x.Model).ToLower().Contains(searchTerm.ToLower())
                    || x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            carsQuery = sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(x => x.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(x => x.Brand).ThenBy(x => x.Model),
                CarSorting.DateCreated or _ => carsQuery.OrderByDescending(x => x.Id)
            };
            var totalCars = carsQuery.Count();
            var cars = GetCars(carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage));

            return new CarQueryServiceModel
            {
                TotalCars = totalCars,
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                Cars = cars
            };
        }

        public IEnumerable<string> AllCarBrands()
            => this.data
                .Cars
                .Select(x => x.Brand)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

        public IEnumerable<CarServiceModel> ByUser(string userId)
            => GetCars(this.data
                .Cars
                .Where(x => x.Dealer!.UserId == userId));



        private static IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carQuery)
            => carQuery.Select(c => new CarServiceModel()
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Year = c.Year,
                CategoryName = c.Category.Name
            })
                .ToList();

        public IEnumerable<CarCategoryServiceModel> AllCarCategories()
            => this.data
                .Categories
                .Select(x => new CarCategoryServiceModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

        public CarDetailsServiceModel Details(int carId)
             => this.data
                .Cars
                .Where(x => x.Id == carId)
                .ProjectTo<CarDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefault()!;

        public bool CategoryExists(int categoryId)
            => this.data
                .Categories
                .Any(c => c.Id == categoryId);

        public int Create(string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId,
            int dealerId)
        {

            var carData = new Car
            {
                Brand = brand,
                Model = model,
                Description = description,
                ImageUrl = imageUrl,
                Year = year,
                CategoryId = categoryId,
                DealerId = dealerId
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            return carData.Id;
        }

        public bool Edit(
            int carId,
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId)
        {

            var carData = this.data.Cars.Find(carId);

            if (carData == null)
            {
                return false;
            }

            carData!.Brand = brand;
            carData.Model = model;
            carData.Description = description;
            carData.ImageUrl = imageUrl;
            carData.Year = year;
            carData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public bool IsByDealer(int carId, int dealerId)
            => this.data
                .Cars
                .Any(x => x.Id == carId && x.DealerId == dealerId);
    }
}

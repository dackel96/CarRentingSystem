using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;
using CarRentingSystem.Models.Cars;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext data;

        public CarsController(ApplicationDbContext data)
            => this.data = data;


        public IActionResult Add() => View(new AddCarFormModel
        {
            Categories = this.GetCarCategories()
        });

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            if (!this.data.Categories.Any(c => c.Id == car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.GetCarCategories();

                return View(car);
            }

            var carData = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {

            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery.Where(x => x.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                carsQuery = carsQuery.Where(x =>
                    (x.Brand + " " + x.Model).ToLower().Contains(query.SearchTerm.ToLower())
                    || x.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            carsQuery = query.Sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(x => x.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(x => x.Brand).ThenBy(x => x.Model),
                CarSorting.DateCreated or _ => carsQuery.OrderByDescending(x => x.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = carsQuery
                .Skip((query.CurrentPage - 1) * AllCarsQueryModel.CarsPerPage)
                .Take(AllCarsQueryModel.CarsPerPage)
                .Select(c => new CarListingViewModel()
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    Category = c.Category.Name
                })
                .ToList();

            var carBrands = this.data
                .Cars
                .Select(x => x.Brand)
                .Distinct()
                .OrderBy(x => x)
                .ToList();


            query.TotalCars = totalCars;
            query.Brands = carBrands;
            query.Cars = cars;

            return View(query);
        }

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
            => this.data
            .Categories
            .Select(c => new CarCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToHashSet();
    }
}

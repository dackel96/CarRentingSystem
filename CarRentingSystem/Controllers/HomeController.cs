using System.Diagnostics;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentingSystem.Data;
using CarRentingSystem.Models;
using CarRentingSystem.Models.Home;
using CarRentingSystem.Services.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public HomeController(IStatisticsService statistics,
            IMapper mapper,
            ApplicationDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var cars = this.data
               .Cars
               .OrderByDescending(c => c.Id)
               .ProjectTo<CarIndexViewModel>(this.mapper.ConfigurationProvider)
               .Take(3)
               .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers,
                Cars = cars
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
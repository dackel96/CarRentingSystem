using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static CarRentingSystem.Areas.Admin.AdminConstants;
namespace CarRentingSystem.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AreaName)]
    public class CarsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

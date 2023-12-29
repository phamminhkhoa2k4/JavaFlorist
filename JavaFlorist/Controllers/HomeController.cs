using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBouquetService _bouquetService;
        public HomeController(IBouquetService bouquetService)
        {
            this._bouquetService = bouquetService;
        }
        // Home Page
        public IActionResult Index()
        {
            var data = _bouquetService.GetTopFiveDistinctCategoriesBySoldCount();
            return View(data);
        }

        // About Page
        public IActionResult About()
        {
            return View();
        }



    }
}

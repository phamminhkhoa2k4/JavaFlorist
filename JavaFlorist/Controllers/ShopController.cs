using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace JavaFlorist.Controllers
{
    public class ShopController : Controller
    {
        private readonly IBouquetService _bouquetService;
        public ShopController(IBouquetService bouquetService)
        {
            _bouquetService = bouquetService;
        }
        public IActionResult Shop(string term = "", int currentPage = 1)
        {
            var bouquet = _bouquetService.List(term, true, currentPage);
            return View("Shop", bouquet);
        }


        public IActionResult ShopDetail(int bouquet_id)
        {
            var bouquet = _bouquetService.GetById(bouquet_id);
            if (bouquet == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy bó hoa
            }

            var relatedBouquets = _bouquetService.GetRelatedBouquets(bouquet_id);

			var bouquetAndRelated = new RelatedModel
            {
                Bouquet = bouquet,
                RelatedBouquets = relatedBouquets
            };
            return View(bouquetAndRelated);
        }


        public IActionResult FilterByCategory(string category = "", bool paging = false, int currentPage = 0)
        {
            var bouquetCategory = _bouquetService.ListCategory(category, true, currentPage);
            return View(bouquetCategory);
        }

        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            var suggestions = _bouquetService.Search(searchTerm);

            return Json(suggestions);
        }



        public IActionResult listCategory()
        {
            var category = _bouquetService.GetDistinctCategories();
            return Json(category); 
        }

        public IActionResult CountBouquet()
        {
            var data = _bouquetService.GetAll();
            int count = data.Count();
            return Json(count);
        }

        public IActionResult CountSold()
        {
            var data = _bouquetService.GetSoldAll();
            var count = data.Sum(e => e.sold);
            return Json(count);
        }
    }
}

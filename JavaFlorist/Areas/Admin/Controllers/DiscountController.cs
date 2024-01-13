using Humanizer.Localisation;
using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JavaFlorist.Areas.Admin.Controllers
{
      [Authorize(Roles = "Admin")]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [Area("Admin")]
        public IActionResult DiscountList(string search)
        {
            var data = this._discountService.List().ToList();

            // Filter data based on search criteria
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(d => d.code.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.SearchTerm = search;
            return View(data);
        }
        [Area("Admin")]

        public IActionResult Add()
        {
            return View();
        }
        [Area("Admin")]

        [HttpPost]
        public IActionResult Add(Discount model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _discountService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(DiscountList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }



        [Area("Admin")]
        public IActionResult Edit(int id)
        {
            var data = this._discountService.GetById(id);
            return View(data);
        }

        [Area("Admin")]
        [HttpPost]
        public IActionResult Edit(Discount model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _discountService.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction(nameof(DiscountList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        [Area("Admin")]
        public IActionResult Delete(int id)
        {
            var result = _discountService.Delete(id);
            if (result)
            {
                TempData["msg"] = "Deleted Successfully";
                return RedirectToAction(nameof(DiscountList));
            }
            else
            {
                TempData["msg"] = "Error on server side or discount has been used";
                return RedirectToAction(nameof(DiscountList));
            }
           
        }
    }
}

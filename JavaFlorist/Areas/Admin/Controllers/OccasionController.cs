using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using JavaFlorist.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OccasionController : Controller
    {
        private readonly IOccassionService _occasionService;
        public OccasionController(IOccassionService occasionService)
        {
            this._occasionService = occasionService;
        }
        [Area("Admin")]
        public IActionResult OccasionList()
        {
           var data = _occasionService.List().ToList();
            
            return View(data);
        }

        [Area("Admin")]

        public IActionResult Add()
        {
            return View();
        }
        [Area("Admin")]

        [HttpPost]
        public IActionResult Add(Occasion model)
        {
            model.IsPattern = true;
            if (!ModelState.IsValid)
                return View(model);

            var result = _occasionService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(OccasionList));
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
            var data = this._occasionService.GetById(id);
            return View(data);
        }

        [Area("Admin")]
        [HttpPost]
        public IActionResult Edit(Occasion model)
        {
            model.IsPattern = true;
            if (!ModelState.IsValid)
                return View(model);

            var result = _occasionService.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction(nameof(OccasionList));
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
            var result = _occasionService.Delete(id);
            return RedirectToAction(nameof(OccasionList));
        }
    }
}


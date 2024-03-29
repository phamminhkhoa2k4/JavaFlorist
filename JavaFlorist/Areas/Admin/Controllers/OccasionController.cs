﻿using JavaFlorist.Models.Domain;
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
        public IActionResult OccasionList(string search)
        {
           var data = _occasionService.List().ToList();
            // Filter data based on search criteria
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(d => d.Occasion_name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
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
            if (result)
            {
                TempData["msg"] = "Deleted Successfully";
                return RedirectToAction(nameof(OccasionList));
            }
            else
            {
                TempData["msg"] = "Error on server side or occasion has been used";
                return RedirectToAction(nameof(OccasionList));
            }
            
        }
    }
}


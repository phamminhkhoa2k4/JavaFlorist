
using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JavaFlorist.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
   
    public class ShopController : Controller
    {
        private readonly IBouquetService _bouquetService;
        private readonly IFileService _fileService;
        public ShopController(IBouquetService BouquetService, IFileService fileService)
        {
            _bouquetService = BouquetService;
            _fileService = fileService;
        }
        [Area("Admin")]
        public IActionResult ShopList()
        {
            var data = this._bouquetService.List();
            return View(data);
        }

        [Area("Admin")]

        public IActionResult Add()
        {
            return View();
        }

        [Area("Admin")]
        [HttpPost]
        public IActionResult Add(Bouquet_Info model)
        {
           
            if (!ModelState.IsValid)
                return View(model);


            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.bouquetImage = imageName;
            }
            var result = _bouquetService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(ShopList));
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
            var model = _bouquetService.GetById(id);
            return View(model);
        }

        [Area("Admin")]
        [HttpPost]
        public IActionResult Edit(Bouquet_Info model)
        {
            
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.bouquetImage = imageName;
            }
            var result = _bouquetService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(ShopList));
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
            var result = _bouquetService.Delete(id);
            return RedirectToAction(nameof(ShopList));
        }
    }
}

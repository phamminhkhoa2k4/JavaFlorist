using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using JavaFlorist.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JavaFlorist.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IFileService _fileService;

        public BlogController(IBlogService blogService , IFileService fileService)
        {
            _blogService = blogService;
            _fileService = fileService;
        }
        [Area("Admin")]
        public IActionResult BlogList()
        {
            var data = this._blogService.List();
            return View(data);
        }


        [Area("Admin")]

        public IActionResult Add()
        {
            return View();
        }

        [Area("Admin")]
        [HttpPost]
        public IActionResult Add(Blog model)
        {
            model.blog_date = DateTime.Now;
            if (!ModelState.IsValid)
                //return View(model);


            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.thumbnail = imageName;
            }
            var result = _blogService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(BlogList));
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
            var model = _blogService.GetById(id);
            return View(model);
        }

        [Area("Admin")]
        [HttpPost]
        public IActionResult Edit(Blog model)
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
                model.thumbnail = imageName;
            }
            var result = _blogService.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction(nameof(BlogList));
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
            var result = _blogService.Delete(id);
            if (result)
            {
                TempData["msg"] = "Deleted Successfully";
                return RedirectToAction(nameof(BlogList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return RedirectToAction(nameof(BlogList));
            }
            
        }

    }
}

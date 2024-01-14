using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JavaFlorist.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [Area("Admin")]
        public IActionResult ContactList(string search)
        {
            var data = this._contactService.List().ToList();
            // Filter data based on search criteria
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(d => d.name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.SearchTerm = search;
            return View(data);
        }

        [Area("Admin")]
        public IActionResult Delete(int id)
        {
            var result = _contactService.Delete(id);
            if (result)
            {
                TempData["msg"] = "Deleted Successfully";
                return RedirectToAction(nameof(ContactList));

            }
            else
            {
                TempData["msg"] = "Error on server side";
                return RedirectToAction(nameof(ContactList));
            }
           
        }

        [Area("Admin")]
        public IActionResult Mark(int id)
        {
            var data = this._contactService.GetById(id);
            data.marked = true;
            var result = _contactService.Update(data);
            if (result)
            {
                return RedirectToAction(nameof(ContactList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return RedirectToAction(nameof(ContactList));
            }
        }
    }
}

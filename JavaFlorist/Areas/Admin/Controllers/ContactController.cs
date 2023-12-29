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
        public IActionResult ContactList()
        {
            var data = this._contactService.List().ToList();
            return View(data);
        }

        [Area("Admin")]
        public IActionResult Delete(int id)
        {
            var result = _contactService.Delete(id);
            return RedirectToAction(nameof(ContactList));
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

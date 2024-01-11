using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Send(Contact model)
        {
            model.marked = false;
            if (!ModelState.IsValid)
                return View(model);

            var result = _contactService.Add(model);
            if (result)
            {
                TempData["msg"] = "Send Successfully";
                return Redirect("/Contact/Contact");
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }


        

    }
}

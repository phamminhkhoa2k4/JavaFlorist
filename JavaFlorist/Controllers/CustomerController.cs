using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}

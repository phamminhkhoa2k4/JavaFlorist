using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist.Controllers
{
    public class CartController : Controller
    {
        [Authorize]
        public IActionResult Cart()
        {
            return View();
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

  

    }

}

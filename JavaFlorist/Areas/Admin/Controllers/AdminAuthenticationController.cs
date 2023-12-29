using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist.Areas.Admin.Controllers
{
    public class AdminAuthenticationController : Controller
    {


        [Authorize(Roles = "Admin")]
        [Area("Admin")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

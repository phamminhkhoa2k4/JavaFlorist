using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JavaFlorist.Areas.Admin.Controllers
{
	public class AdministrationController : Controller
	{
		[AdminAuthorize]
        [Authorize(Roles = "Admin")]
        [Area("Admin")]
		public IActionResult Index()
		{
			return View();
		}

      

        
    }
}

using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JavaFlorist.Areas.Admin.Controllers
{
        [Authorize(Roles = "Admin")]
	public class AdministrationController : Controller
	{
	  private readonly IOrderService _orderService;

        public AdministrationController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Area("Admin")]
		public IActionResult Index()
		{
            var data = _orderService.GetLatestOrder();

            return View(data);
        }

      

        
    }
}

using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JavaFlorist.Areas.Admin.Controllers
{
        [Authorize(Roles = "Admin")]
    public class CartController : Controller
    {
        private readonly IOrderService _orderService;
        public CartController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Area("Admin")]
        public IActionResult OrderList()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

    }

}

using JavaFlorist.Models.Domain;
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
        public IActionResult OrderList(string search)
        {
            var orders = _orderService.GetAllOrders();
            // Filter data based on search criteria
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(d => d.Delivery_Info.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.SearchTerm = search;
            return View(orders);
        }


        [Area("Admin")]
        public IActionResult OrderDetail(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound(); 
            }
            return View(order); 
        }

        [Area("Admin")]
        public IActionResult UpdateStatusDelivery(int orderId , string status)
        {
            if(status == "Confirm" || status == "Delivery" || status == "Delivered" || status == "Received")
            {
                var result = _orderService.UpdateStatus(orderId ,status);
                if (result)
                {
                    return Redirect(Request.Headers["referer"].ToString());
                }
                else
                {
                    return Redirect(Request.Headers["referer"].ToString());
                }

            }

            return Redirect(Request.Headers["referer"].ToString());
        }


        [Area("Admin")]
        public IActionResult DeleteOrder(int orderId) {
           var result =  _orderService.Delete(orderId);
           if (result)
            {
                TempData["msg"] = "Deleted Successfully";
                return RedirectToAction(nameof(OrderList));

            }
            else
            {
                TempData["msg"] = "Error on server side";
                return RedirectToAction(nameof(OrderList));
            }
        }


        [Area("Admin")]

        public IActionResult MonthEarning()
        {
            var receivedOrdersTotal = _orderService.GetAllTotalByReceivedStatus();
            var totalSum = receivedOrdersTotal.Sum();
            return Json(totalSum);
        }




    }

}

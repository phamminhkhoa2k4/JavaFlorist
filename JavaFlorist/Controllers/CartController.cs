using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using JavaFlorist.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JavaFlorist.Controllers
{
        [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IBouquetService _bouquetService;
        private readonly IOccassionService _occasionService;
        private readonly IOrderService _orderService;
        private readonly IDiscountService _discountService;


        public CartController(ICartService cartService, IBouquetService bouquetService, IOccassionService occasionService , IOrderService orderService , IDiscountService discountService 
  )
		{
			_cartService = cartService;
			_bouquetService = bouquetService;
			_occasionService = occasionService;
            _orderService = orderService;
            _discountService = discountService;
		}

		
        public IActionResult Cart(int cust_id)
        {
            var cart = _cartService.GetCart(cust_id);
            return View(cart);
        }


		public IActionResult Count(int cust_id)
		{
			var cart = _cartService.GetCart(cust_id);
			int itemCount = cart.Items.Count;
			return Json(itemCount);
		}



		public IActionResult AddToCart(int bouquetId, int cust_id , int quantity = 1 )
        {
            var bouquet = _bouquetService.GetById(bouquetId); 
            if (bouquet != null)
            {
                _cartService.AddItemToCart(bouquet, quantity ,cust_id);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
		public IActionResult AddToCartd(int bouquetId, int cust_id, int quantity = 1)
		{
			var bouquet = _bouquetService.GetById(bouquetId);
			if (bouquet != null)
			{
				_cartService.AddItemToCart(bouquet, quantity, cust_id);
			}
			return Redirect(Request.Headers["Referer"].ToString());
		}


		public IActionResult RemoveFromCart(int bouquetId, int cust_id)
        {
            var bouquet = _bouquetService.GetById(bouquetId); 
            if (bouquet != null)
            {
                _cartService.RemoveItemFromCart(bouquet, cust_id);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult ClearCart(int cust_id)
        {
            _cartService.ClearCart(cust_id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

		public IActionResult UpdateCartItemQuantity(int bouquetId , int newQuantity , int cust_id)
		{
				_cartService.UpdateQuantity(bouquetId, newQuantity ,cust_id);
			
			// Cập nhật số lượng sản phẩm trong giỏ hàng
			return Redirect(Request.Headers["Referer"].ToString());
		}
		public IActionResult Checkout(int cust_id)
		{

			var model = new Order();
			model.OccasionList = _occasionService.List()
    .Select(a => new SelectListItem { Text = a.Occasion_name, Value = a.Occasion_name }).Distinct();
            model.cart = _cartService.GetCart(cust_id);
			return View(model);
		}


		public IActionResult MessagePattern(string occasionName)
		{
            var pattern = _occasionService.SubList(occasionName).Select(p => p.message).Distinct();
            return Json(pattern);
        }

		[HttpPost]
		public IActionResult Checkout(Order  model)
        {
          


            DateTime currentTime = DateTime.Now;

            TimeSpan startOfWorkingHours = new TimeSpan(9, 0, 0);
            TimeSpan endOfWorkingHours = new TimeSpan(21, 0, 0); 

            bool isWithinWorkingHours = currentTime.TimeOfDay >= startOfWorkingHours && currentTime.TimeOfDay <= endOfWorkingHours;

            DateTime orderTime =  DateTime.Now; 

            TimeSpan timeElapsedSinceOrder = currentTime - orderTime;

            if (timeElapsedSinceOrder.TotalHours <= 5 && isWithinWorkingHours)
            {
                model.Delivery_Info.Date = currentTime.Date; 
            }
            else
            {
                model.Delivery_Info.Date = currentTime.Date.AddDays(1); 
            }

            var result = _orderService.Add(model);
            if (result)
            {
                _cartService.ClearCart(model.cust_id);
                if (model.discount_id != null && model.discount_id != 0)
                {
					_discountService.Decrease((int)model.discount_id);
				}
                TempData["order"] = true;
                TempData["msg"] = "You have placed your order successfully and are waiting for confirmation!";
				TempData["RegisteredSuccessfully"] = true;
				return RedirectToAction("Cart", "Cart", new { cust_id = model.cust_id });

			}
			else
            {
                return Redirect(Request.Headers["referer"].ToString());
            }

			

		}


        public IActionResult MyOrder(int cust_id)
        {   
            var data = _orderService.GetAllOrdersByCustomerId(cust_id);
            return View(data);
        }

        public IActionResult OrderDetail(int OrderId)
        {
            var order = _orderService.GetOrderById(OrderId);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }


        public IActionResult UpdateStatusDelivery(int orderId, string status)
        {
            if (status == "Received")
            {
                var result = _orderService.UpdateStatus(orderId, status);
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



        public IActionResult CountOrder()
        {
            var data = _orderService.GetAllOrders();
            var count = data.Count();
            return Json(count);
        }

    }

}

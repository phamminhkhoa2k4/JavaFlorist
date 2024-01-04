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


		public CartController(ICartService cartService, IBouquetService bouquetService, IOccassionService occasionService , IOrderService orderService)
		{
			_cartService = cartService;
			_bouquetService = bouquetService;
			_occasionService = occasionService;
            _orderService = orderService;
		}

		
        public IActionResult Cart(int cust_id)
        {
            var cart = _cartService.GetCart(cust_id);
            return View(cart);
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
			model.OccasionList = _occasionService.List().Select(a => new SelectListItem { Text = a.Occasion_name, Value = a.Occasion_id.ToString() });
			model.cart = _cartService.GetCart(cust_id);
			return View(model);
		}


		[HttpPost]
		public IActionResult Checkout(Order  model)
        {
            model.OccasionList = _occasionService.List().Select(a => new SelectListItem { Text = a.Occasion_name, Value = a.Occasion_id.ToString() });
            //if (!ModelState.IsValid)
            //    return View(model);



            var result = _orderService.Add(model);
            if (result)
            {
                _cartService.ClearCart(model.cust_id);

			}
			else
            {
				return View(model);
			}

			return RedirectToAction("Cart", "Cart", new { cust_id = model.cust_id });

		}



	}

}

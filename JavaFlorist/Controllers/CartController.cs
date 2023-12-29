using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist.Controllers
{
        [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IBouquetService _bouquetService;

        public CartController(ICartService cartService, IBouquetService bouquetService)
        {
            _cartService = cartService;
            _bouquetService = bouquetService;
        }

        public IActionResult Cart()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        public IActionResult AddToCart(int bouquetId, int quantity = 1)
        {
            var bouquet = _bouquetService.GetById(bouquetId); 
            if (bouquet != null)
            {
                _cartService.AddItemToCart(bouquet, quantity);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult RemoveFromCart(int bouquetId)
        {
            var bouquet = _bouquetService.GetById(bouquetId); 
            if (bouquet != null)
            {
                _cartService.RemoveItemFromCart(bouquet);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return Redirect(Request.Headers["Referer"].ToString());
        }

		public IActionResult UpdateCartItemQuantity(int bouquetId , int newQuantity)
		{

			
				_cartService.UpdateQuantity(bouquetId, newQuantity);
			
			// Cập nhật số lượng sản phẩm trong giỏ hàng
			return Redirect(Request.Headers["Referer"].ToString());


		}

		public IActionResult Checkout()
        {
            return View();
		}




    }

}

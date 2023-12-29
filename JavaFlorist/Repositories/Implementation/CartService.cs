using JavaFlorist.Infrastructure;
using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Http;

namespace JavaFlorist.Repositories.Implementation
{
    public class CartService : ICartService
    {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private ISession _session => _httpContextAccessor.HttpContext.Session;
		private const string CartSessionKey = "Cart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
			_httpContextAccessor = httpContextAccessor;
		}
        public void AddItemToCart(Bouquet_Info bouquet, int quantity)
        {
            var cart = GetCart();
            cart.AddItem(bouquet, quantity);
            SaveCart(cart);
        }

        public int CalculateTotalValue()
        {
            var cart = GetCart();
            return cart.TotolValue();
        }

        public void ClearCart()
        {
            var cart = new Cart();
            SaveCart(cart); ;
        }

        public Cart GetCart()
        {
            return _session.GetJson<Cart>(CartSessionKey) ?? new Cart();
        }
    

        public void RemoveItemFromCart(Bouquet_Info bouquet)
        {
            var cart = GetCart();
            cart.RemoveItem(bouquet);
            SaveCart(cart);
        }

        private void SaveCart(Cart cart)
        {
            _session.SetJson(CartSessionKey, cart);
        }

		public void UpdateQuantity(int bouquetId, int newQuantity)
		{
			var cart = GetCart();
			cart.UpdateQuantity(bouquetId, newQuantity);
			SaveCart(cart);
		}
	}
}

using JavaFlorist.Infrastructure;
using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Http;

namespace JavaFlorist.Repositories.Implementation
{
    public class CartService : ICartService
    {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private ISession _session => _httpContextAccessor.HttpContext.Session;


        public CartService(IHttpContextAccessor httpContextAccessor)
        {
			_httpContextAccessor = httpContextAccessor;
		}
        public void AddItemToCart(Bouquet_Info bouquet, int quantity , int cust_id)
        {
           
            var cart = GetCart(cust_id);
            cart.AddItem(bouquet, quantity , cust_id);
            SaveCart(cart, cust_id);
        }

        public int CalculateTotalValue(int cust_id)
        {
            var cart = GetCart(cust_id);
            return cart.TotolValue();
        }

        public void ClearCart(int cust_id)
        {
            var cart = new Cart();
            SaveCart(cart, cust_id); ;
        }

        public Cart GetCart(int cust_id)
        {
			string cartKey = $"Cart_{cust_id}";
			return _session.GetJson<Cart>(cartKey) ?? new Cart();
        }
    

        public void RemoveItemFromCart(Bouquet_Info bouquet , int cust_id)
        {
            var cart = GetCart(cust_id);
            cart.RemoveItem(bouquet);
            SaveCart(cart, cust_id);
        }

		private void SaveCart(Cart cart, int cust_id)
		{
			string cartKey = $"Cart_{cust_id}"; 
			_session.SetJson(cartKey, cart);
		}


		public void UpdateQuantity(int bouquetId, int newQuantity, int cust_id)
		{
            var cart = GetCart(cust_id);
			cart.UpdateQuantity(bouquetId, newQuantity );
			SaveCart(cart, cust_id);
		}
	}
}

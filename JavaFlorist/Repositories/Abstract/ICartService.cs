using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;

namespace JavaFlorist.Repositories.Abstract
{
    public interface ICartService
    {
        Cart GetCart();
        void AddItemToCart(Bouquet_Info bouquet, int quantity);
        void RemoveItemFromCart(Bouquet_Info bouquet);
        int CalculateTotalValue();
        void ClearCart();
        void UpdateQuantity(int bouquetId, int newQuantity);

	}
}

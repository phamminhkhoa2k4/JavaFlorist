using JavaFlorist.Models.Domain;

namespace JavaFlorist.Repositories.Abstract
{
    public interface ICartService
    {
        Cart GetCart(int cust_id);
        void AddItemToCart(Bouquet_Info bouquet, int quantity ,int cust_id);
        void RemoveItemFromCart(Bouquet_Info bouquet, int cust_id);
        int CalculateTotalValue(int cust_id);
        void ClearCart(int cust_id);
        void UpdateQuantity(int bouquetId, int newQuantity , int cust_id);

	}
}

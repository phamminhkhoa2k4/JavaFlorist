using JavaFlorist.Models.Domain;


namespace JavaFlorist.Models.DTO
{
    public class CartModel
    {
        public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();
        
     
    }

}

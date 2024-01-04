using JavaFlorist.Models.Domain;

namespace JavaFlorist.Models.DTO
{
    public class CartItemModel
    {
        public int CartItem_id { get; set; }
        public Bouquet_Info Bouquet { get; set; }
        public int Quantity { get; set; }

        public int cust_id { get; set; } 

        public int SubTotal { get; set; }


        
    }
}

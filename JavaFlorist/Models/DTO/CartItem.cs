using JavaFlorist.Models.Domain;

namespace JavaFlorist.Models.DTO
{
    public class CartItem
    {
        public int CartItem_id { get; set; }
        public Bouquet_Info Bouquet { get; set; }
        public int Quantity { get; set; }
    }
}

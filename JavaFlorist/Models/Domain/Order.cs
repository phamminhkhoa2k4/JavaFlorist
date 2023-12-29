using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }

        [ForeignKey("Cart")]
        public int cart_id { get; set; }

        [ForeignKey("Delivery_Info")]
        public int delivery_id { get; set; }

        public DateTime order_date { get; set; }

        [ForeignKey("Discount")]
        public int discount_id { get; set; }
    }
}

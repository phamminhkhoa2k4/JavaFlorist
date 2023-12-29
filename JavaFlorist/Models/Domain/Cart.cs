using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Cart
    {
        [Key]
        public int cart_id { get; set; }

        [ForeignKey("Customer")]
        public int cust_id { get; set; }

        [ForeignKey("Bouquet_Info")]
        public int bouquet_id { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public int total_price { get; set; }
    }

}

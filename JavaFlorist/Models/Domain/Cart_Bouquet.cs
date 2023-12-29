using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Cart_Bouquet
    {
        [Key]
        public int cart_bouquet_id { get; set; }

        [ForeignKey("Cart")]
        public int cart_id { get; set; }

        [ForeignKey("Bouquet_Info")]
        public int bouquet_id { get; set; }
    }
}

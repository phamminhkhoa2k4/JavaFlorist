using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Wishlist
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public int wishlist_id { get; set; }

        [ForeignKey("Bouquet_Info")]
        [Column(TypeName = "numeric(18, 0)")]
        public int bouquet_id { get; set; }

        [ForeignKey("Customer")]
        [Column(TypeName = "numeric(18, 0)")]
        public int cust_id { get; set; }

        // Navigation property for Bouquet_Info
        public Bouquet_Info Bouquet_Info { get; set; }


    }
}

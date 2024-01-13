using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
	public class CartItem
	{

        [Column(TypeName = "numeric(18, 0)")]
        public int CartItemId { get; set; }

		[ForeignKey("Bouquet_Info")]
        [Column(TypeName = "numeric(18, 0)")]
        public int bouquet_id { get; set; }

		public Bouquet_Info Bouquet { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public int Quantity { get; set; }

        [Column(TypeName = "numeric(18, 2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public int cust_id { get; set; }
	}
}

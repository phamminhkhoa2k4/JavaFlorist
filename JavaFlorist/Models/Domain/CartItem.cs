using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
	public class CartItem
	{
		public int CartItemId { get; set; }

		[ForeignKey("Bouquet_Info")]
		public int bouquet_id { get; set; }
		public Bouquet_Info Bouquet { get; set; }
		public int Quantity { get; set; }
	    
		public int SubTotal { get; set; }
		public int cust_id { get; set; }
	}
}

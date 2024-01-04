using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
	public class Order
	{
		[Key]
		public int order_id { get; set; }
		public DateTime order_date { get; set; } //
		 
		public int Total { get; set; }

		[ForeignKey("Customer")]
		public int cust_id { get; set; } //
		public Customer Customer { get; set; }

		[ForeignKey("Discount")]
		public int? discount_id { get; set; } //
		public Discount? Discount { get; set; }


		public int delivery_id { get; set; }
		public Delivery_Info Delivery_Info { get; set; } //

		[ForeignKey("Cart")]
		public int CartId { get; set; }
		public Cart Cart { get; set; }


		[ForeignKey("Occasion")]
		public int Occasion_id { get; set; }

		public Occasion Occasion { get; set; } // 



		[NotMapped]
		public IEnumerable<SelectListItem>? OccasionList { get; set; }

		[NotMapped]
		public MultiSelectList? MultiOccasionList { get; set; }

		[NotMapped]
		public Cart cart { get; set; }
	}
}

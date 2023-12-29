using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Delivery_Info
    {
        [Key]
        public int delivery_id { get; set; }

        [ForeignKey("Cart")]
        public int cart_id { get; set; }

        [ForeignKey("Occasion")]
        public int Occasion_id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        public int Phone { get; set; }

        public DateTime Delivery_Date { get; set; }

        [Required]
        public string? Delivery_status { get; set; }
    }
}

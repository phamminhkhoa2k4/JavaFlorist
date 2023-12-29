using System.ComponentModel.DataAnnotations;

namespace JavaFlorist.Models.Domain
{
    public class Discount
    {
        [Key]
        public int discount_id { get; set; }

        [Required]
        public string? code { get; set; }

        public int count { get; set; }

        public int decrease { get; set; }
    }
}

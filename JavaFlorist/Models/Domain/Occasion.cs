using System.ComponentModel.DataAnnotations;

namespace JavaFlorist.Models.Domain
{
    public class Occasion
    {
        [Key]
        public int Occasion_id { get; set; }

        [Required]
        public string? Occasion_name { get; set; }

        public string? message { get; set; }
    }
}

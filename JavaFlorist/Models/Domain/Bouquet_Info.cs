using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Bouquet_Info
    {
        [Key]
        public int bouquet_id { get; set; }



        [Required]
        public string name { get; set; }
        public string? bouquetImage { get; set; }  

        [Required]
        public int price { get; set; }


        public int sold { get; set; }


        [Required]
        public int quantity { get; set; }

        [Required]
        public string? category { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}

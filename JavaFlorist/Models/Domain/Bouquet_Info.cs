using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Bouquet_Info
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public int bouquet_id { get; set; }



        [Required]
        [StringLength(maximumLength: 50)]
        [Column(TypeName = "varchar(50)")]
        public string name { get; set; }

        [StringLength(maximumLength: 1000)]
        [Column(TypeName = "varchar(max)")]
        public string? bouquetImage { get; set; }  

        [Required]
        [Column(TypeName = "numeric(18, 2)")]
        public decimal price { get; set; }


        public int sold { get; set; }


        [Required]
        [MaxLength]
        [Column(TypeName = "text")]
        public string description { get; set; }

        [Required]
        [StringLength(maximumLength: 255)]
        [Column(TypeName = "varchar(255)")]
        public string? category { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
 
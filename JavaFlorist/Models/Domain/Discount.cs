using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Discount
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public int discount_id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        [Column(TypeName = "varchar(50)")]
        public string? code { get; set; }

        [Required]
        [Column(TypeName = "numeric(18, 0)")]
        public int count { get; set; }


        [Required]
        [Column(TypeName = "numeric(18, 2)")]
        public decimal decrease { get; set; }
    }
}

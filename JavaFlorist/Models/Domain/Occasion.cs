using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Occasion
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public int Occasion_id { get; set; }

        [Required]
        [StringLength(maximumLength: 255)]
        [Column(TypeName = "varchar(255)")]
        public string? Occasion_name { get; set; }

        [Required]
        [StringLength(maximumLength: 1000)]
        [Column(TypeName = "varchar(max)")]
        public string? message { get; set; }

        public bool? IsPattern { get; set; }
    }
}

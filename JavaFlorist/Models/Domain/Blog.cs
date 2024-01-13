using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Blog
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public int blog_id { get; set; }

        [Required]
        [StringLength(maximumLength: 1000)]
        [Column(TypeName = "varchar(max)")]
        public string? thumbnail { get; set; }

        [Required]
        [StringLength(maximumLength: 1000)]
        [Column(TypeName = "varchar(max)")]
        public string? title { get; set; }

        [Required]
        [MaxLength]
        [Column(TypeName = "text")]
        public string? content { get; set; }

        public DateTime blog_date { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}

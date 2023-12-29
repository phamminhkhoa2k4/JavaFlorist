using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Blog
    {
        [Key]
        public int blog_id { get; set; }

        [Required]
        public string? thumbnail { get; set; }

        [Required]
        public string? title { get; set; }

        [Required]
        public string? content { get; set; }

        public DateTime blog_date { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}

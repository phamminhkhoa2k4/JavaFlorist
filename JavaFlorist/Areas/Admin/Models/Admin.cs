using System.ComponentModel.DataAnnotations;

namespace JavaFlorist.Areas.Admin.Models
{
    public class Admin
    {
        [Key]
        public int admin_id { get; set; }
        [Required]
        public string? avatar { get; set; }

        [Required]
        public string? username { get; set; }

        [Required]
        public string? password { get; set; }
    }
}

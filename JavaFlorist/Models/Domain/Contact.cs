using System.ComponentModel.DataAnnotations;

namespace JavaFlorist.Models.Domain
{
    public class Contact
    {
        [Key]
        public int contact_id { get; set; }

        [Required]
        public string? name { get; set; }

        [Required]
        [EmailAddress]
        public string? email { get; set; }

        [Required]
        public string? subject { get; set; }

        [Required]
        public string? message { get; set; }

        [Required]
        public bool? marked { get; set; }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Contact
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public int contact_id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        [Column(TypeName = "varchar(50)")]
        public string? name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(maximumLength: 255)]
        [Column(TypeName = "varchar(255)")]
        public string? email { get; set; }

        [Required]
        [StringLength(maximumLength: 255)]
        [Column(TypeName = "varchar(255)")]
        public string? subject { get; set; }

        [Required]
        [MaxLength]
        [Column(TypeName = "text")]
        public string? message { get; set; }

    
        public bool? marked { get; set; }
    }
}

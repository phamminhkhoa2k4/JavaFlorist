using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(maximumLength: 1000)]
        [Column(TypeName = "varchar(max)")]
        public string?   ProfilePicture { get; set; }
        [ForeignKey("Customer")]
        [Column(TypeName = "numeric(18, 0)")]
        public int cust_id { get; set; } // Khóa ngoại đến Customer

        public Customer Customer { get; set; } // Navigation property

        
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        //public string? Name{ get; set; }
        public string?   ProfilePicture { get; set; }
        [ForeignKey("Customer")]
        public int cust_id { get; set; } // Khóa ngoại đến Customer

        public Customer Customer { get; set; } // Navigation property


    }
}

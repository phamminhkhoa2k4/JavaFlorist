using System.ComponentModel.DataAnnotations;

namespace JavaFlorist.Models.Domain
{
    public class Customer
    {
        [Key]
        public int cust_id { get; set; }

        public string? F_name { get; set; }

        public string? L_name { get; set; }

        public DateTime Dob { get; set; }

        
        [StringLength(1)]
        public string? Gender { get; set; }

        public int P_no { get; set; }

        public string? Address { get; set; }
    }
}

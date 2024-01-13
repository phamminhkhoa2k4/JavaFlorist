using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Customer
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public int cust_id { get; set; }

        [StringLength(maximumLength: 50)]
        [Column(TypeName = "varchar(50)")]
        public string? F_name { get; set; }

        [StringLength(maximumLength: 50)]
        [Column(TypeName = "varchar(50)")]
        public string? L_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime Dob { get; set; }

        
        [StringLength(1)]
        [Column(TypeName = "char(10)")]
        public string? Gender { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long P_no { get; set; }

        [StringLength(maximumLength: 1000)]
        [Column(TypeName = "varchar(max)")]
        public string? Address { get; set; }
    }
}

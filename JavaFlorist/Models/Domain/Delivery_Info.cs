using JavaFlorist.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Delivery_Info
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public int delivery_id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        [Column(TypeName = "varchar(50)")]
        public string? Name { get; set; }

        [Required]
        [StringLength(maximumLength: 1000)]
        [Column(TypeName = "varchar(max)")]
        public string? Address { get; set; }

        [Required]
        [Column(TypeName = "numeric(18, 0)")]
        public long Phone { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [StringLength(maximumLength: 50)]
        [Column(TypeName = "varchar(50)")]
        public string Delivery_status { get; set; }

		


	}
}

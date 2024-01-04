using JavaFlorist.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
    public class Delivery_Info
    {
        [Key]
        public int delivery_id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }
        [Required]
        public long Phone { get; set; }

        [Required]
        public DateTime Date { get; set; }


		


	}
}

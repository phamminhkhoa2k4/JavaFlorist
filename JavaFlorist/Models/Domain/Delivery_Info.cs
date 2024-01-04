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

		
        public string? Name { get; set; }


        public string? Address { get; set; }

        public int Phone { get; set; }


		public DateTime Date { get; set; }


		


	}
}

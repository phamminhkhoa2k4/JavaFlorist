using JavaFlorist.Models.Domain;

namespace JavaFlorist.Models.DTO
{
    public class HomeModel
    {
        public List<Bouquet_Info> topFiveProducts { get; set; } 
        public List<Bouquet_Info> topProducts { get; set; } 

    }
}

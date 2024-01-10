using JavaFlorist.Models.Domain;

namespace JavaFlorist.Models.DTO
{
    public class DashboardModel
    {
        public List<Order> order { get; set; }
        public List<Bouquet_Info>  bouquets { get; set; }
    }
}

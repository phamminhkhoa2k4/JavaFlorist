using JavaFlorist.Models.Domain;

namespace JavaFlorist.Models.DTO
{
    public class RelatedModel
    {
        public Bouquet_Info Bouquet { get; set; } // Thông tin sản phẩm chi tiết
        public List<Bouquet_Info> RelatedBouquets { get; set; } // Danh sách sản phẩm liên quan
    }
}

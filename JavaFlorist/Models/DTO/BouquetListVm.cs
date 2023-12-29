using JavaFlorist.Models.Domain;


namespace JavaFlorist.Models.DTO
{
    public class BouquetListVm
    {
        public IQueryable<Bouquet_Info> BouquetList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}

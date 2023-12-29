using JavaFlorist.Models.Domain;

namespace JavaFlorist.Models.DTO
{
    public class BlogListVm
    {
        public IQueryable<Blog> BlogList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}

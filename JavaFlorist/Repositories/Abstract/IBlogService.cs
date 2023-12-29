using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;

namespace JavaFlorist.Repositories.Abstract
{
    public interface IBlogService
    {
        bool Add(Blog model);
        bool Update(Blog model);
        Blog GetById(int id);
        bool Delete(int id);
        //IQueryable<Blog> List();

        BlogListVm List(string term = "", bool paging = false, int currentPage = 0);

    }
}

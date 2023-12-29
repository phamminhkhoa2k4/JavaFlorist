using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;

namespace JavaFlorist.Repositories.Abstract
{
    public interface IBouquetService
    {
        bool Add(Bouquet_Info model);
        bool Update(Bouquet_Info model);
        Bouquet_Info GetById(int id);
        bool Delete(int id);
        BouquetListVm List(string term = "", bool paging = false, int currentPage = 0);
        BouquetListVm ListCategory(string category = "", bool paging = false, int currentPage = 0);

        List<Bouquet_Info> Search(string searchTerm);

        List<Bouquet_Info> GetRelatedBouquets(int bouquet_id);
        List<Bouquet_Info> GetByCategory(string category);

		List<HomeModel> GetTopDistinctCategoriesBySoldCount();

	}
}

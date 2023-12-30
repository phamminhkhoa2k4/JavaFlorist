using JavaFlorist.Models.Domain;

namespace JavaFlorist.Repositories.Abstract
{
    public interface IOccassionService
    {
        bool Add(Occasion model);
        bool Update(Occasion model);
        Occasion GetById(int id);
        bool Delete(int id);
        IQueryable<Occasion> List();
    }
}


using JavaFlorist.Models.Domain;

namespace JavaFlorist.Repositories.Abstract
{
    public interface IContactService
    {
        bool Add(Contact model);
        Contact GetById(int id);
        bool Update(Contact model);
        bool Delete(int id);
        IQueryable<Contact> List();
    }
}

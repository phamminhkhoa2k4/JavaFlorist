using JavaFlorist.Models.Domain;
namespace JavaFlorist.Repositories.Abstract
{
    public interface IDiscountService
    {
        bool Add(Discount model);
        bool Update(Discount model);
        Discount GetById(int id);
        bool Delete(int id);
        IQueryable<Discount> List();
        Discount GetDiscountByCode(string code);
        bool Decrease(int id);
    }
}

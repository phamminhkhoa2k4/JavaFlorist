using JavaFlorist.Models.Domain;

namespace JavaFlorist.Repositories.Abstract
{
    public interface IWishlistService
    {
        Task<List<Wishlist>> GetList(int id);
        bool Add(Wishlist model);
        Wishlist GetById(int id);
        bool Delete(int id);

    }
}

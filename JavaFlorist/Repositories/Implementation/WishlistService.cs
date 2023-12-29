using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace JavaFlorist.Repositories.Implementation
{
    public class WishlistService : IWishlistService
    {
        private readonly DatabaseContext ctx;
        public WishlistService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Wishlist model)
        {
            try
            {
                var existingItem = ctx.Wishlist.FirstOrDefault(w => w.cust_id == model.cust_id && w.bouquet_id == model.bouquet_id);

                if (existingItem == null)
                {
                    ctx.Wishlist.Add(model);
                    ctx.SaveChanges();
                    return true; // Thêm mới thành công vì không có bản ghi trùng
                }
                else
                {
                    return false; // Đã tồn tại bản ghi có cust_id và bouquet_id tương tự
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                ctx.Wishlist.Remove(data);
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public Wishlist GetById(int id)
        {
            return ctx.Wishlist.Find(id);
        }

        public async Task<List<Wishlist>> GetList(int id)
        {

            var data = await ctx.Wishlist
                .Include(w => w.Bouquet_Info)
                .Where(w => w.cust_id == id)
                .ToListAsync();
            return data;
        }
    }
}

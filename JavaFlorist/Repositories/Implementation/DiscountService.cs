using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;

namespace JavaFlorist.Repositories.Implementation
{
    public class DiscountService : IDiscountService
    {
        private readonly DatabaseContext ctx;

        public DiscountService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Discount model)
        {
            try
            {
                ctx.Discount.Add(model);
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
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
                ctx.Discount.Remove(data);
                ctx.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
          
        }

        public Discount GetById(int id)
        {
            return ctx.Discount.Find(id);
        }

        public IQueryable<Discount> List()
        {
           var data = ctx.Discount.AsQueryable();
            return data;

        }

        public bool Update(Discount model)
        {
            try
            {
                ctx.Discount.Update(model);
                ctx.SaveChanges();
                return true;
            }catch( Exception ex)
            {
                return false;
            }
        }
    }
}

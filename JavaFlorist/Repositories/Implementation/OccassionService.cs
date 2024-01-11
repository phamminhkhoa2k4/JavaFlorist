using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;

namespace JavaFlorist.Repositories.Implementation
{
    public class OccassionService : IOccassionService
    {
        private readonly DatabaseContext ctx;

        public OccassionService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Occasion model)
        {
            try
            {
                ctx.Occassion.Add(model);
                ctx.SaveChanges();
                return true;
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
                ctx.Occassion.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Occasion GetById(int id)
        {
            return ctx.Occassion.Find(id);
        }

        public Discount GetDiscountByCode(string code)
        {
            return ctx.Discount.FirstOrDefault(d => d.code == code);
        }

        public IQueryable<Occasion> List()
        {
            var data = ctx.Occassion.Where(o => (bool)o.IsPattern).AsQueryable();
            return data;

        }


		public IQueryable<Occasion> SubList(string occasionName)
		{
			var data = ctx.Occassion
		    .Where(o => (bool)o.IsPattern && o.Occasion_name == occasionName).AsQueryable();

			return data;

		}


		public bool Update(Occasion model)
        {
            try
            {
                ctx.Occassion.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

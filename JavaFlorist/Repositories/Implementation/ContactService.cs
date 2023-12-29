using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;

namespace JavaFlorist.Repositories.Implementation
{
    public class ContactService : IContactService
    {
        private readonly DatabaseContext ctx;
        public ContactService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Contact model)
        {
            try
            {
                ctx.Contact.Add(model);
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
                ctx.Contact.Remove(data);
                ctx.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public Contact GetById(int id)
        {
            return ctx.Contact.Find(id);
        }

        public IQueryable<Contact> List()
        {
            var data = ctx.Contact.AsQueryable();
            return data;
        }

        public bool Update(Contact model)
        {
            try
            {
                ctx.Contact.Update(model);
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

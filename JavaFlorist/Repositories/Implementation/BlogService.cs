using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;

using System.Linq.Expressions;

namespace JavaFlorist.Repositories.Implementation
{
    public class BlogService : IBlogService
    {
        private readonly DatabaseContext ctx;
        
        public BlogService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Add(Blog model)
        {
            try
            {
                ctx.Blog.Add(model);
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
                if(data == null)
                return false;
                ctx.Blog.Remove(data);
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public Blog GetById(int id)
        {
            return ctx.Blog.Find(id);
        }

        public IQueryable<Blog> List()
        {
            var data = ctx.Blog.AsQueryable();
            return data;
        }
        public BlogListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new BlogListVm();

            var list = ctx.Blog.ToList();


            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.title.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                // here we will apply paging
                int pageSize = 6;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }


            data.BlogList = list.AsQueryable();
            return data;
        }
        public bool Update(Blog model)
        {
            try
            {
                ctx.Blog.Update(model);
                ctx.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}

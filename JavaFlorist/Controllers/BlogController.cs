using JavaFlorist.Repositories.Abstract;
using JavaFlorist.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace JavaFlorist.Controllers
{

    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Blog(string term = "", int currentPage = 1)
        {
            var blog = _blogService.List(term, true, currentPage);
            return View(blog);
        }

        public IActionResult BlogDetail(int id)
        {
            var blog = _blogService.GetById(id);
            return View(blog);
        }

    

    }
}

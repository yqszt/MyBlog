using BlogBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using My_Blog.Models;
using System.Linq;

namespace My_Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogManager manager;

        public HomeController(BlogManager blogManager)
        {
            manager = blogManager;
        }

        public ActionResult Index()
        {
            var posts = manager.GetTop5().Select(post => new PostViewModel()
            {
                Author = post.Author,
                Content = post.Content,
                CreateDate = post.CreateDate,
                ID = post.ID,
                ModifyDate = post.ModifyDate,
                Title = post.Title
            }).ToList();
            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
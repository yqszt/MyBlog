using System.Linq;
using BlogModel;
using BlogBusinessLogic;
using My_Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace My_Blog.Controllers
{
    public class PostController : Controller
    {
        private BlogManager manager;

        public PostController(BlogManager blogManager)
        {
            manager = blogManager;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var posts = manager.GetAllPosts().Select(post => new PostViewModel()
            {
                Author = post.Author,
                Content = post.Content,
                CreateDate = post.CreateDate,
                ID = post.ID,
                ModifyDate = post.ModifyDate,
                Title = post.Title
            }).ToList();
            var postListViewModel = new PostListViewModel()
            {
                Count = posts.Count,
                PageCount = 1,
                Pages = 1,
                Posts = posts
            };
            
            return View(postListViewModel);
        }

        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <returns></returns>
        public ActionResult Get(int id)
        {
              var post = manager.GetPostById(id);
            if (post == null)
                return this.StatusCode(404);
              var postViewModel = new PostViewModel()
              {
                  Author = post.Author,
                  Content = post.Content,
                  CreateDate = post.CreateDate,
                  ID = post.ID,
                  ModifyDate = post.ModifyDate,
                  Title = post.Title
              };
              return View(postViewModel);
        }
    }
}
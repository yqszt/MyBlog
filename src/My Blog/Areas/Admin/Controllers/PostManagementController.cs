using BlogBusinessLogic;
using BlogModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Blog.Areas.Admin.Models;
using System;
using System.IO;
using System.Linq;

namespace My_Blog.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class PostManagementController : Controller
    {
        private BlogManager manager;

        public PostManagementController(BlogManager manager)
        {
            this.manager = manager;
        }
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var posts = manager.GetAllPosts().Select(post => new PostMaintainViewModel()
            {
                Content = post.Content,
                ID = post.ID,
                Title = post.Title
            }).ToList();
            var postListViewModel = new PostMaintainListViewModel()
            {
                Count = posts.Count,
                PageCount = 1,
                Pages = 1,
                Posts = posts
            };

            return View(postListViewModel);
        }

        public ActionResult Update(int id)
        {
            var post = manager.GetPostById(id);
            if (post == null)
                return this.StatusCode(404);
            var postViewModel = new PostMaintainViewModel()
            {
                Content = post.Content,
                ID = post.ID,
                Title = post.Title
            };
            return View(postViewModel);
        }

        [HttpPost]
        public ActionResult Update(PostMaintainViewModel postModel)
        {
            var post = manager.GetPostById(postModel.ID);
            post.Content = postModel.Content;
            post.Title = postModel.Title;
            post.ModifyDate = DateTime.Now;
            manager.UpdatePost(post);
            return RedirectToAction("Index");
        }

        public ActionResult Insert()
        {
            var postViewModel = new PostMaintainViewModel();
            return View(postViewModel);
        }

        [HttpPost]
        public ActionResult Insert(PostMaintainViewModel postModel)
        {
            var post = new Post()
            {
                Title = postModel.Title,
                Content = postModel.Content,
                Author = "7米鱼",
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                IsPublish = true
            };
            manager.Insert(post);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var post = manager.GetPostById(id);
            manager.Delete(post);
            return RedirectToAction("Index");
        }

       /* public ActionResult FileUpload()
        {
            string fileName = "";
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase postFile = Request.Files[file];//get post file 
                if (postFile.ContentLength == 0)
                    continue;
                string strWebPath = "/Upload/Temp/";
                string locationpath = Server.MapPath(strWebPath);
                if (!Directory.Exists(locationpath))
                {
                    Directory.CreateDirectory(locationpath);
                }
                fileName = DateTime.Now.Millisecond + Path.GetFileName(postFile.FileName);
                postFile.SaveAs(locationpath + fileName);//save file 
            }
            var url = "http://" + HttpContext.Request.Url.Authority + "/Upload/Temp/" + fileName;
            return Json(new { FileUrl = url, FileName = fileName });
        }*/
    }
}
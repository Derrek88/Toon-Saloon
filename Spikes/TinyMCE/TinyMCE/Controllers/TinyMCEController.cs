using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyMCE.Models;
using TinyMCE.repo;

namespace TinyMCE.Controllers
{
    public class TinyMCEController : Controller
    {
        // GET: TinyMCE
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewPosts()
        {
            var repo = new BlogPostRepo();
            var posts = repo.GetAllBlogPosts();
            return View(posts);
        }

        
        [HttpPost]
        public ActionResult AddPost(BlogPost post)
        {
            var repo = new BlogPostRepo();
            repo.AddBlogPost(post);
            return RedirectToAction("ViewPosts");
        }
    }
}
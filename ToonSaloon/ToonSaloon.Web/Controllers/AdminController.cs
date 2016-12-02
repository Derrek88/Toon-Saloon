using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToonSaloon.BLL;
using ToonSaloon.Models;

namespace ToonSaloon.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
   
        //BLOG POST SECTION
        [HttpGet]
        public ActionResult ManageCurrentPosts()
        {
            var model = new PostManager().GetAllPosts();
            return View(model);
        }

        [HttpGet]
        public ActionResult AdminAddPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminAddPost(BlogPost post, IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    var filename = System.IO.Path.GetFileName(file.FileName);

                    // Where do we want to save the image
                    var path = System.IO.Path.Combine(Server.MapPath("~/App_Data/uploads"), filename);
                    file.SaveAs(path);
                }
            }
            //var repo = new BlogPostRepo();
            //repo.AddBlogPost(post);
            return RedirectToAction("ManageCurrentPosts");
        }

        [HttpGet]
        public ActionResult AdminEditPost()
        {
            return View();
        }


        //TOON OF THE DAY SECTION
        [HttpGet]
        public ActionResult ManageToonOfTheDay()
        {
            return View();
        }

        //SUBMISSIONS SECTION
        [HttpGet]
        public ActionResult AdminViewBlogPostSubmissions()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdminViewToonOfTheDaySubmissions()
        {
            return View();
        }
    }
}
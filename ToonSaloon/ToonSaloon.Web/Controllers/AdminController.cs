﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToonSaloon.BLL;
using ToonSaloon.BLL.Managers;
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
                
                if (file != null && file.ContentLength > 0)
                {
                    var filename = System.IO.Path.GetFileName(file.FileName);

                    // Where do we want to save the image
                    var path = System.IO.Path.Combine(Server.MapPath("~/Post Images"), filename);
                    file.SaveAs(path);
                }
            }

            var manager = new PostManager();
            manager.AddBlogPost(post);
            return RedirectToAction("ManageCurrentPosts");
        }

        [HttpGet]
        public ActionResult AdminEditPost(int id)
        {
            var model = new PostManager().GetPostByID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AdminEditPost(BlogPost post)
        {
            var manager = new PostManager();
            manager.AddBlogPost(post);
            return RedirectToAction("ManageCurrentPosts");
        }

        [HttpGet]
        public ActionResult AdminDeletetPost(int id)
        {
            var model = new PostManager().GetPostByID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AdminDeletetPost(BlogPost post)
        {
            var manager = new PostManager();
            manager.RemoveBlogPost(post);
            return RedirectToAction("ManageCurrentPosts");
        }
    

        //TOON OF THE DAY SECTION
        [HttpGet]
        public ActionResult ManageToonOfTheDay()
        {
            var manager = new ToonOfTheDayManager();
            var model = manager.GetAllToons();
            return View(model);
        }

        [HttpGet]
        public ActionResult AdminAddToon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminAddToon(CartoonOfTheDay toon)
        {
            var manager = new ToonOfTheDayManager();
            manager.AddToonOfDay(toon);
            return RedirectToAction("ManageToonOfTheDay");
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
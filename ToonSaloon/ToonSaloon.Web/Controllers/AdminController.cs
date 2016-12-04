using System;
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
            int i = 0;
            foreach (var file in files)
            {
                
                if (file != null && file.ContentLength > 0)
                {
                    var filename = System.IO.Path.GetFileName(file.FileName);

                    // Where do we want to save the image
                    var path = System.IO.Path.Combine(Server.MapPath("../Images/appimages"), filename);
                    file.SaveAs(path);
                    post.Imgs[i].Source = "../../Images/appimages/" + filename;

                }
                i++;
            }

            var manager = new PostManager();
            post.Approved = Approved.Yes;
            post.DateCreated = DateTime.Today;
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
            manager.EditBlogPost(post);
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
        public ActionResult AdminAddToon(CartoonOfTheDay toon, HttpPostedFileBase file)
        {
            var filename = System.IO.Path.GetFileName(file.FileName);
            var path = System.IO.Path.Combine(Server.MapPath("../Images/appimages"), filename);
            file.SaveAs(path);
            toon.ImgUrl = "../../Images/appimages/" + filename;

            var manager = new ToonOfTheDayManager();
            manager.AddToonOfDay(toon);
            return RedirectToAction("ManageToonOfTheDay");
        }

        [HttpGet]
        public ActionResult AdminEditToon(int id)
        {
            var model = new ToonOfTheDayManager().GetCartoonOfTheDay(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AdminEditToon(CartoonOfTheDay toon)
        {
            var manager = new ToonOfTheDayManager();
            manager.EditToonOfDay(toon); ;
            return RedirectToAction("ManageToonOfTheDay");
        }

        [HttpGet]
        public ActionResult AdminDeleteToon(int id)
        {
            var model = new ToonOfTheDayManager().GetCartoonOfTheDay(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AdminDeleteToon(CartoonOfTheDay toon)
        {
            var manager = new ToonOfTheDayManager();
            manager.RemoveToonOfDay(toon);
            return RedirectToAction("ManageToonOfTheDay");
        }



        //SUBMISSIONS SECTION

        [HttpGet]
        public ActionResult AdminViewBlogPostSubmissions()
        {
            var repo = new PostManager();
            var model = repo.GetUnapprovedBlogPosts();
            return View(model);
        }

        [HttpGet]
        public ActionResult AdminViewToonOfTheDaySubmissions()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult AdminViewPost(int id)
        //{

        //    var repo = new PostManager();
        //    var model = repo.GetPostByID(id);

        //    return View(model);
        //}
        [HttpPost]
        public ActionResult AdminApprovePost(BlogPost post)
        {
            var manager = new PostManager();
            manager.EditBlogPost(post);

            return RedirectToAction("AdminViewBlogPostSubmissions");

        }

        [HttpGet]
        public ActionResult AdminApprovePost(int id)
        {

            var manager = new PostManager();
            var model = manager.GetPostByID(id);

            return View(model);
        }
    }
}
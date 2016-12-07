using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToonSaloon.BLL;
using ToonSaloon.Models;

namespace ToonSaloon.Web.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ManageCurrentPosts()

        {
            var repo = new PostManager();
            var model = repo.GetAllPosts();

            return View(model);
        }

        [HttpGet]
        public ActionResult EmployeeAddPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeAddPost(BlogPost post, IEnumerable<HttpPostedFileBase> files)
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
            post.Approved = Approved.Waiting;
            post.DateCreated = DateTime.Today;
            manager.AddBlogPost(post);
            return RedirectToAction("ManageCurrentPosts", "Employee");
        }
        [HttpGet]
        public ActionResult EmployeeEditPost(int id)
        {
            var model = new PostManager().GetPostByID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EmployeeEditPost(BlogPost post)
        {
            var manager = new PostManager();
            manager.EditBlogPost(post);
            return RedirectToAction("ManageCurrentPosts", "Employee");
        }

        [HttpGet]
        public ActionResult EmployeeDeletetPost(int id)
        {
            var model = new PostManager().GetPostByID(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EmployeeDeletetPost(BlogPost post)
        {
            var manager = new PostManager();
            manager.RemoveBlogPost(post);

            return RedirectToAction("ManageCurrentPosts", "Employee");
        }

        [HttpGet]
        public ActionResult ManageToonOfTheDay()
        {
            var repo = new ToonOfTheDayManager();
            var model = repo.GetAllToons();
            return View(model);
        }

        [HttpGet]
        public ActionResult EmployeeAddToon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeAddToon(CartoonOfTheDay toon, HttpPostedFileBase file)
        {
            var filename = System.IO.Path.GetFileName(file.FileName);
            var path = System.IO.Path.Combine(Server.MapPath("../Images/appimages"), filename);
            file.SaveAs(path);

            toon.ImgUrl = "../../Images/appimages/" + filename;
            toon.Approved = Approved.Waiting;
            toon.DateCreated = DateTime.Today;

            var manager = new ToonOfTheDayManager();
            manager.AddToonOfDay(toon);

            return RedirectToAction("ManageToonOfTheDay", "Employee");
        }

        [HttpGet]
        public ActionResult EmployeeEditToon(int id)
        {
            var model = new ToonOfTheDayManager().GetCartoonOfTheDay(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EmployeeEditToon(CartoonOfTheDay toon)
        {
            var manager = new ToonOfTheDayManager();
            manager.RemoveToonOfDay(toon);
            return RedirectToAction("ManageToonOfTheDay", "Employee");
        }

        [HttpGet]
        public ActionResult EmployeeDeleteToon(int id)
        {
            var model = new ToonOfTheDayManager().GetCartoonOfTheDay(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EmployeeDeleteToon(CartoonOfTheDay toon)
        {
            var manager = new ToonOfTheDayManager();
            manager.RemoveToonOfDay(toon);
            return RedirectToAction("ManageToonOfTheDay", "Employee");
        }

        [HttpGet]
        public ActionResult EmployeeViewPandR()
        {
            var manager = new StaticManger();
            var model = manager.GetPostByID(3);
            return View(model);
        }
    }
}
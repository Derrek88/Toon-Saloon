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
        [Authorize(Roles = "Employee")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult ManageCurrentPosts()

        {
            var repo = new PostManager();
            var model = repo.GetAllPosts();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeAddPost()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeAddPost(BlogPost post, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
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
            return View("EmployeeAddPost");
        }
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeEditPost(int id)
        {
            var model = new PostManager().GetPostByID(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeEditPost(BlogPost post)
        {
            if (ModelState.IsValid)
            {
                var manager = new PostManager();
                manager.EditBlogPost(post);
                return RedirectToAction("ManageCurrentPosts", "Employee");
            }
            return View("EmployeeEditPost");
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeDeletetPost(int id)
        {
            var model = new PostManager().GetPostByID(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeDeletetPost(BlogPost post)
        {
            var manager = new PostManager();
            manager.RemoveBlogPost(post);

            return RedirectToAction("ManageCurrentPosts", "Employee");
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult ManageToonOfTheDay()
        {
            var repo = new ToonOfTheDayManager();
            var model = repo.GetAllToons();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeAddToon()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeAddToon(CartoonOfTheDay toon, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
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
            return View("EmployeeAddToon");
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeEditToon(int id)
        {
            var model = new ToonOfTheDayManager().GetCartoonOfTheDay(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeEditToon(CartoonOfTheDay toon)
        {
            if (ModelState.IsValid)
            {
                var manager = new ToonOfTheDayManager();
                manager.RemoveToonOfDay(toon);
                return RedirectToAction("ManageToonOfTheDay", "Employee");
            }
            return View("EmployeeEditToon");
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeDeleteToon(int id)
        {
            var model = new ToonOfTheDayManager().GetCartoonOfTheDay(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeDeleteToon(CartoonOfTheDay toon)
        {
            var manager = new ToonOfTheDayManager();
            manager.RemoveToonOfDay(toon);
            return RedirectToAction("ManageToonOfTheDay", "Employee");
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeViewPandR()
        {
            var manager = new StaticManger();
            var model = manager.GetPostByID(3);
            return View(model);
        }
    }
}
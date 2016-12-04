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
        public ActionResult ManageToonOfTheDay()
        {
            var repo = new ToonOfTheDayManager();
            var model = repo.GetAllToons();
            return View(model);
        }

        public ActionResult EmployeeViewPandR()
        {
            return View();
        }

        public ActionResult EmployeeAddPost(BlogPost post, IEnumerable<HttpPostedFileBase> files)
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
            post.Approved = Approved.Waiting;
            manager.AddBlogPost(post);
            return RedirectToAction("ManageCurrentPosts");
        }

        public ActionResult EmployeeEditPost(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult EmployeeDeletetPost(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult EmployeeEditToon(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult EmployeeDeleteToon(int id)
        {
            throw new NotImplementedException();
        }
    }
}
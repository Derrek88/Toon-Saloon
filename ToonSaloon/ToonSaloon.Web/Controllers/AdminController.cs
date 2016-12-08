using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToonSaloon.BLL;
using ToonSaloon.BLL.Managers;
using ToonSaloon.Models;
using ToonSaloon.Models.Models;

namespace ToonSaloon.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
   
        //BLOG POST SECTION
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ManageCurrentPosts()
        {
            var model = new PostManager().GetAllPosts();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminAddPost()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminAddPost(BlogPost post, IEnumerable<HttpPostedFileBase> files)
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
                post.Approved = Approved.Yes;
                post.DateCreated = DateTime.Today;
                manager.AddBlogPost(post);
                return RedirectToAction("ManageCurrentPosts");
            }
            return View("AdminAddPost");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminEditPost(int id)
        {
            string holder = "";
            var model = new PostManager().GetPostByID(id);

            foreach (var tag in model.Tags)
            {
                holder = $"{holder},{tag.Name}";
            }
            model.TagPlaceHolder = holder;
            return View(model);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminEditPost(BlogPost post)
        {
            if (ModelState.IsValid)
            {
                var manager = new PostManager();
                manager.EditBlogPost(post);
                return RedirectToAction("ManageCurrentPosts");
            }
            return View("AdminEditPost");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDeletetPost(int id)
        {
            var model = new PostManager().GetPostByID(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDeletetPost(BlogPost post)
        {
            var manager = new PostManager();
            manager.RemoveBlogPost(post);
            return RedirectToAction("ManageCurrentPosts");
        }
    

        //TOON OF THE DAY SECTION
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ManageToonOfTheDay()
        {
            var manager = new ToonOfTheDayManager();
            var model = manager.GetAllToons();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminAddToon()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminAddToon(CartoonOfTheDay toon, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var filename = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("../Images/appimages"), filename);
                file.SaveAs(path);

                toon.ImgUrl = "../../Images/appimages/" + filename;
                toon.Approved = Approved.Yes;
                toon.DateCreated = DateTime.Today;

                var manager = new ToonOfTheDayManager();
                manager.AddToonOfDay(toon);
                return RedirectToAction("ManageToonOfTheDay");
            }
            return View("AdminAddToon");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminEditToon(int id)
        {
            var model = new ToonOfTheDayManager().GetCartoonOfTheDay(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminEditToon(CartoonOfTheDay toon)
        {
            if (ModelState.IsValid)
            {
                var manager = new ToonOfTheDayManager();
                manager.EditToonOfDay(toon);
                ;
                return RedirectToAction("ManageToonOfTheDay");
            }
            return View("AdminEditPost");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDeleteToon(int id)
        {
            var model = new ToonOfTheDayManager().GetCartoonOfTheDay(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDeleteToon(CartoonOfTheDay toon)
        {
            var manager = new ToonOfTheDayManager();
            manager.RemoveToonOfDay(toon);
            return RedirectToAction("ManageToonOfTheDay");
        }



        //SUBMISSIONS SECTION

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminViewBlogPostSubmissions()
        {
            var repo = new PostManager();
            var model = repo.GetUnapprovedBlogPosts();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminViewToonOfTheDaySubmissions()
        {
            var manager = new ToonOfTheDayManager();
            var model = manager.GetUnapprovedToons();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult AdminViewPost(int id)
        //{

        //    var repo = new PostManager();
        //    var model = repo.GetPostByID(id);

        //    return View(model);
        //}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminApprovePost(BlogPost post)
        {
            var manager = new PostManager();
            manager.EditBlogPost(post);

            return RedirectToAction("AdminViewBlogPostSubmissions");

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminApprovePost(int id)
        {

            var manager = new PostManager();
            var model = manager.GetPostByID(id);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminApproveToon(int id)
        {
            var manager = new ToonOfTheDayManager();
            var model = manager.GetCartoonOfTheDay(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminApproveToon(CartoonOfTheDay toon)
        {
            var manager = new ToonOfTheDayManager();
            manager.EditToonOfDay(toon);
            return RedirectToAction("AdminViewToonOfTheDaySubmissions");
        }

        //STATIC PAGE SECTION

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ManageStaticPages()
        {
            var manager = new StaticManger();
            var model = manager.GetAllPages();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminAddStaticPage()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminAddStaticPage(StaticPage page)
        {
            var manager = new StaticManger();
            manager.AddStaticPage(page);
            return RedirectToAction("ManageStaticPages");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminStaticPageWithPosts()
        {
            var manager = new TagManager();
            var vm = new StaticPageSearchVM();
            vm.SetTags(manager.GetAllTags());
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminStaticPageWithPosts(StaticPageSearchVM vm)
        {
            if (ModelState.IsValid)
            {
                var manager = new StaticManger();
                var tagm = new TagManager();
                StaticPage page = new StaticPage();
                var pagetags = new List<Tag>();
                page.Name = vm.Page.Name;
                page.Tag = vm.Page.Tag;
                page.Body = vm.Page.Body;
                page.Category = vm.Page.Category;
                page.Name = vm.Page.Name;
                page.Approved = Approved.Yes;
                page.DateCreated = DateTime.Today;
                foreach (var id in vm.SelectedTagIds)
                {
                    var tag = tagm.GetTagById(id);
                    pagetags.Add(tag);

                }
                page.Tag = pagetags;
                manager.AddStaticPage(page);
                return RedirectToAction("ManageStaticPages");
            }
            return View("AdminStaticPageWithPosts");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminEditStaticPage(int id)
        {
            var tagmanager = new TagManager();
            var manager = new StaticManger();
            var page = manager.GetPostByID(id);
            var vm = new StaticPageSearchVM();
            vm.SetTags(tagmanager.GetAllTags());

            vm.Page = page;

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminEditStaticPage(StaticPageSearchVM vm)
        {
            if (ModelState.IsValid)
            {
                var manager = new StaticManger();
                var tagm = new TagManager();
                var page = new StaticPage();

                var pagetags = new List<Tag>();
                page.Name = vm.Page.Name;
                page.Tag = vm.Page.Tag;
                page.Category = vm.Page.Category;
                page.Body = vm.Page.Body;
                page.Name = vm.Page.Name;
                page.Approved = Approved.Yes;
                page.DateCreated = DateTime.Today;
                page.Id = vm.Page.Id;
                foreach (var id in vm.SelectedTagIds)
                {
                    var tag = tagm.GetTagById(id);
                    pagetags.Add(tag);

                }
                page.Tag = pagetags;

                manager.EditStaticPage(page);
                return RedirectToAction("ManageStaticPages");
            }
            return View("AdminEditStaticPage");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDeleteStaticPage(int id)
        {
            var manager = new StaticManger();
            var model = manager.GetPostByID(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDeleteStaticPage(StaticPage page)
        {
            var manager = new StaticManger();
            manager.RemoveStaticPage(page);
            return RedirectToAction("ManageStaticPages");
        }
    }
}
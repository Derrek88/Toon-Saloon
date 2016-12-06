using System.Web.Mvc;
using ToonSaloon.BLL;

namespace ToonSaloon.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new PostManager().GetAllPosts();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult ViewSearchedStaticPage(int id)
        {
            var manager = new StaticManger();
            var model = manager.GetBySearch(id);
        }

    }
}
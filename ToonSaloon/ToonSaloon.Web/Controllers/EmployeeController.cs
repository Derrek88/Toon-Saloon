using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToonSaloon.Web.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageCurrentPosts()
        {
            return View();
        }

        public ActionResult ManageToonOfTheDay()
        {
            return View();
        }

        public ActionResult EmployeeViewPandR()
        {
            return View();
        }
    }
}
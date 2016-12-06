using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToonSaloon.BLL;
using ToonSaloon.Models;

namespace ToonSaloon.Web.Controllers
{
    public class StaticPageListController : ApiController
    {
        [HttpGet]
        public List<StaticPage> PagesList()
        {
            var manager = new StaticManger();
            var list = manager.GetAllPages();
            //list.RemoveRange(0, 3);

            return list;
        } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToonSaloon.BLL.Managers;
using ToonSaloon.Models;

namespace ToonSaloon.Web.Controllers
{
    
    public class PopularTagsController : ApiController
    {
        [HttpGet]
        public List<Tag> Populartags()
        {
            var manager = new TagManager();

            return manager.GetTopTenTags();

        } 
    }
}

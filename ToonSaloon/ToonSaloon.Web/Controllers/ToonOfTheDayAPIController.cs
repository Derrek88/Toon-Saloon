using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToonSaloon.Models;
using ToonSaloon.BLL;

namespace ToonSaloon.Web.Controllers
{
    public class ToonOfTheDayAPIController : ApiController
    {
        public CartoonOfTheDay GetToon()
        {
            var manager = new ToonOfTheDayManager();

            return manager.GetToonOfTheDay();
        }
    }
}



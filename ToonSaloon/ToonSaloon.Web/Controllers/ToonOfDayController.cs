using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using ToonSaloon.BLL;
using ToonSaloon.Models;

namespace ToonSaloon.Web.Controllers
{
    //[KnownType(typeof(ToonSaloon.Models.Approved))]
    public class ToonOfDayController : ApiController
    {
        public CartoonOfTheDay Get()
        {

            var manager = new ToonOfTheDayManager(); 
            var toon =  manager.ChooseCartoon();
            toon.HasNotBeenPosted = false;
            if (toon.WhenPosted == default(DateTime))
            {
                toon.WhenPosted = DateTime.Now;
            }
            
            return toon;


        }
    }
}

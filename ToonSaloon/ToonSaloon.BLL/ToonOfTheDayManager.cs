using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.InMemRepo;
using ToonSaloon.Models;

namespace ToonSaloon.BLL
{
    public class ToonOfTheDayManager
    {
        public CartoonOfTheDay GetToonOfTheDay()
        {
            var repo = new InMemToonOfDayRepo();

            return repo.GetToon();
        }
    }
}

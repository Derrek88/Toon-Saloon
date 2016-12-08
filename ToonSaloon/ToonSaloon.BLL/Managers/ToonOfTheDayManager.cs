using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.Factories;
using ToonSaloon.Data.InMemRepo;
using ToonSaloon.Data.Interface;
using ToonSaloon.Models;

namespace ToonSaloon.BLL
{
    public class ToonOfTheDayManager
    {
        public CartoonOfTheDay ChooseCartoon()
        {
            var repo = ToonOfDayFactory.CreateToonOfDayRepository();
            var toons = repo.GetAllToons();

            var result = from t in toons
                         where t.Approved == Approved.Yes && t.WhenPosted <= DateTime.Now.AddDays(-1)
                         select t;

            var theToon = result.OrderBy(t => t.DateCreated).FirstOrDefault();

            if (theToon == null)
            {
                var getNewUnposted = from t in toons
                              where t.Approved == Approved.Yes && t.HasNotBeenPosted
                              select t;

                Random rnd = new Random();
                var rlist = getNewUnposted.ToList();

                if (rlist.Any())
                {
                    int r = rnd.Next(rlist.Count);

                    return rlist[r];
                }
                
            }
            
            
            if (theToon == null)
            {
                var recycle = from t in toons
                              where t.Approved == Approved.Yes && t.HasNotBeenPosted == false
                              select t;

                Random rnd = new Random();
                var rlist = recycle.ToList();

                int r = rnd.Next(rlist.Count);

                return rlist[r];
            }
            return theToon;
        }



        //}
        public CartoonOfTheDay GetCartoonOfTheDay(int id)
        {
            //ToonOfDayResponse response = new ToonOfDayResponse();

            var repo = ToonOfDayFactory.CreateToonOfDayRepository();
            return repo.GetPostByID(id);

            //if (toon != null)
            //{
            //    response.Success = true;
            //    response.Message = "It worked!";
            //    response.ToonOfTheDay = toon;
            //}
            //else
            //{
            //    response.Success = false;
            //    response.Message = "Not Found!";
            //}
            //return toon;
        }

        public void AddToonOfDay(CartoonOfTheDay toonToAdd)
        {
            var repo = ToonOfDayFactory.CreateToonOfDayRepository();
            toonToAdd.WhenPosted = DateTime.Today;
            repo.AddToonOfDay(toonToAdd);
        }

        public void RemoveToonOfDay(CartoonOfTheDay toonToRemove)
        {

            var repo = ToonOfDayFactory.CreateToonOfDayRepository();
            repo.RemoveToonOfDay(toonToRemove);
        }

        public void EditToonOfDay(CartoonOfTheDay toonToEdit)
        {
            var repo = ToonOfDayFactory.CreateToonOfDayRepository();
            repo.EditToonOfDay(toonToEdit);
        }

        public List<CartoonOfTheDay> GetAllToons()
        {
            var repo = ToonOfDayFactory.CreateToonOfDayRepository();

            return repo.GetAllToons();

        }

        public List<CartoonOfTheDay> GetUnapprovedToons()
        {
            var repo = ToonOfDayFactory.CreateToonOfDayRepository();

            var allToons = repo.GetAllToons();

            var results = from p in allToons
                          where p.Approved == Approved.Waiting
                          select p;

            return results.ToList();
        }
    }
}

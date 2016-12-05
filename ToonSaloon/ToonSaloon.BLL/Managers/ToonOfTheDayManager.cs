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

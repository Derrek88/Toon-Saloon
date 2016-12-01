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
    public class ToonOfTheDayManager : IToonOfDayRepository
    {
        private IToonOfDayRepository _toon;

        public ToonOfTheDayManager()
        {
            _toon = ToonOfDayFactory.CreateToonOfDayRepository();
        }
        public CartoonOfTheDay GetToonOfTheDay()
        {
            var repo = new InMemToonOfDayRepo();

            return repo.GetToon();
        }

        public CartoonOfTheDay GetPostByID(int id)
        {
            ToonOfDayResponse response = new ToonOfDayResponse();
            var toon = _toon.GetPostByID(id);

            if (toon != null)
            {
                response.Success = true;
                response.Message = "It worked!";
            }
            else
            {
                response.Success = false;
                response.Message = "Not Found!";
            }
            return toon;
        }

        public ToonOfDayResponse AddToonOfDay(CartoonOfTheDay toonToAdd)
        {
            ToonOfDayResponse response = new ToonOfDayResponse();
            var toon = _toon.AddToonOfDay(toonToAdd);

            if (toon != null)
            {
                response.Success = true;
                response.Message = "Post Added!";
            }
            else
            {
                response.Success = false;
                response.Message = "Post wasnt added!";
            }
            return toon;
        }

        public ToonOfDayResponse RemoveToonOfDay(CartoonOfTheDay toonToRemove)
        {
            ToonOfDayResponse response = new ToonOfDayResponse();
            var toon = _toon.RemoveToonOfDay(toonToRemove);

            if (toon != null)
            {
                response.Success = true;
                response.Message = "Post Removed!";
            }
            else
            {
                response.Success = false;
                response.Message = "Post wasnt removed!";
            }
            return toon;
        }

        public ToonOfDayResponse EditToonOfDay(CartoonOfTheDay toonToEdit)
        {
            ToonOfDayResponse response = new ToonOfDayResponse();
            var toon = _toon.EditToonOfDay(toonToEdit);

            if (toon != null)
            {
                response.Success = true;
                response.Message = "Post Edited!";
            }
            else
            {
                response.Success = false;
                response.Message = "Post wasnt edited!";
            }
            return toon;
        }
    }
}

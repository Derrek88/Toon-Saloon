using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.Interface;
using ToonSaloon.Models;

namespace ToonSaloon.Data.InMemRepo
{
    public class InMemToonOfDayRepo : IToonOfDayRepository
    {
        private static List<CartoonOfTheDay> _toons;

        public InMemToonOfDayRepo()
        {
            if (_toons == null)
            {
                _toons = new List<CartoonOfTheDay>()
                {
                    new CartoonOfTheDay()

                    {
                        Id = 1,
                        Author = "Bryant",
                        DateCreated = Convert.ToDateTime("11/28/2016"),
                        Season = 2,
                        Episode = 4,
                        ShowName = "Rick and Morty",
                        Approved = Approved.Waiting,
                        ImgUrl = "../../Images/appimages/api.jpg",
                        HasNotBeenPosted = true


                    },

                    new CartoonOfTheDay()
                    {
                        Id = 2,
                        Author = "Derrek",
                        DateCreated = Convert.ToDateTime("12/04/2016"),
                        Season = 1,
                        Episode = 1,
                        ShowName = "Gundam Wing",
                        Approved = Approved.Yes,
                        ImgUrl = "../../Images/appimages/1_ms_2.png",
                        HasNotBeenPosted = true

                    }
                };

            }

        }

        public CartoonOfTheDay GetPostByID(int id)
        {
            return _toons.FirstOrDefault(p => p.Id == id);
            //We need to grab the toon that is approved and is the oldest approved post
            //var result = toonList.Where(p => p.isApproved = true).LastOrDefault();
            //this should get it working now
        }

        public void AddToonOfDay(CartoonOfTheDay toonToAdd)
        {
            _toons.Add(toonToAdd);
        }

        public void RemoveToonOfDay(CartoonOfTheDay toonToRemove)
        {
            var result = _toons.FirstOrDefault(p => p.Id == toonToRemove.Id);
            _toons.Remove(result);
        }

        public void EditToonOfDay(CartoonOfTheDay toonToEdit)
        {
            var post = _toons.FirstOrDefault(p => p.Id == toonToEdit.Id);
            _toons.Remove(post);
            post = toonToEdit;
            _toons.Add(post);
        }

        public List<CartoonOfTheDay> GetAllToons()
        {
            return _toons;
        }

    }
}

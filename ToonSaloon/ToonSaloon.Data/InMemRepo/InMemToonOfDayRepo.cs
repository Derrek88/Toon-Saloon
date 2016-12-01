﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Models;

namespace ToonSaloon.Data.InMemRepo
{
    public class InMemToonOfDayRepo
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
                        isApproved = true,
                        ImgUrl = "../../Images/appimages/watch 1.jpg"

                    },
                };

            }

        }

        public CartoonOfTheDay GetToon()
        {
            var toonList = _toons;
            //We need to grab the toon that is approved and is the oldest approved post
            var result = toonList.Where(p => p.isApproved = true);
                
            return result.First();
        }
    }
}
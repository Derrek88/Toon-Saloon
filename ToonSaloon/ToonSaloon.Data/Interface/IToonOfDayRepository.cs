﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Models;

namespace ToonSaloon.Data.Interface
{
   public interface IToonOfDayRepository
    {
        CartoonOfTheDay GetPostByID(int id);

        ToonOfDayResponse AddToonOfDay(CartoonOfTheDay toonToAdd);

        ToonOfDayResponse RemoveToonOfDay(CartoonOfTheDay toonToRemove);

        ToonOfDayResponse EditToonOfDay(CartoonOfTheDay toonToEdit);
    }
}

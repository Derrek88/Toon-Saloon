using System;
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

        void AddToonOfDay(CartoonOfTheDay toonToAdd);

        void RemoveToonOfDay(CartoonOfTheDay toonToRemove);

        void EditToonOfDay(CartoonOfTheDay toonToEdit);

        List<CartoonOfTheDay> GetAllToons();
    }
}

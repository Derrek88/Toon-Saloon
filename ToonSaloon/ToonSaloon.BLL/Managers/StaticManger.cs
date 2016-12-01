using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.Factories;
using ToonSaloon.Data.Interface;
using ToonSaloon.Models;
using ToonSaloon.Models.Responses;

namespace ToonSaloon.BLL
{
    public class StaticManger 
    {
        public StaticPage GetPostByID(int id)
        {
            StaticResponse response = new StaticResponse();
            var repo = StaticFactory.CreateStaticPageRepository();
            var page = repo.GetPageByID(id);

            if (page != null)
            {
                response.Success = true;
                response.Message = "It worked!";
                response.StaticPage = page;
            }
            else
            {
                response.Success = false;
                response.Message = "Post not found!";
            }
            return page;
        }

        public List<StaticPage> GetAllPages()
        {
            var repo = StaticFactory.CreateStaticPageRepository();
            var page = repo.GetAllPages();
            return page;
        }

        public void AddStaticPage(StaticPage pageToAdd)
        {
            var repo = StaticFactory.CreateStaticPageRepository();
            
            repo.AddStaticPage(pageToAdd);
        }

        public void RemoveStaticPage(StaticPage pageToRemove)
        {
            var repo = StaticFactory.CreateStaticPageRepository();

            repo.RemoveStaticPage(pageToRemove);
        }

        public void EditStaticPage(StaticPage pageToEdit)
        {
            var repo = StaticFactory.CreateStaticPageRepository();

            repo.EditStaticPage(pageToEdit);
        }
    }
}

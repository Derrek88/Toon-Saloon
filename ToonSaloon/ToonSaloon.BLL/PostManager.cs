using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.InMemRepo;
using ToonSaloon.Models;

namespace ToonSaloon.BLL
{
    public class PostManager
    {
        public List<BlogPost> GetAllPosts()
        {
            var repo = new InMemBlogRepo();
            return repo.GetAllPosts();
        }  
    }
}

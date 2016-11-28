using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Models;

namespace ToonSaloon.Data
{
   public interface IBlogPostRepository
   {
       BlogPost GetPostByID();

       List<BlogPost> GetAllPosts();

       void AddBlogPost();

       void RemoveBlogPost();

       void EditBlogPost();
   }
}

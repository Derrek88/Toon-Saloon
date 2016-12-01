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
       BlogPost GetPostByID(int id);

       List<BlogPost> GetAllPosts();

       PostResponse AddBlogPost(BlogPost postToAdd);

       PostResponse RemoveBlogPost(BlogPost postToRemove);

       PostResponse EditBlogPost(BlogPost postToEdit);
   }
}

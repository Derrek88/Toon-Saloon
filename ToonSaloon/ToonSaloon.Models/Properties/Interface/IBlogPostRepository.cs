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

       void AddBlogPost(BlogPost postToAdd);

       void RemoveBlogPost(BlogPost postToRemove);

       void EditBlogPost(BlogPost postToEdit);

       List<BlogPost> GetPostByTag(string TagName);
   }
}

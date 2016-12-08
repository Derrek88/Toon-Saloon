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

       void AddImageToBlogPost(Img imgToAdd, int blogid);

       void InsertImgBlogBridgeTable(int imgId, int newBlogId);

       void RemoveImageToBlogPost(Img imgToDelete);

       void DeleteImgBlogBridgeTable(BlogPost id);

       void InsertTagBlogBridgeTable(int tagId, int newBlogId);

       void DeleteTagBlogBridgeTable(BlogPost id);
   }
}

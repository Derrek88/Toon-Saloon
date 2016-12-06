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

       void AddImageToBlogPost(Img imgToAdd);

       void InsertImgBlogBridgeTable(BlogPost id);

       void RemoveImageToBlogPost(Img imgToDelete);

       void DeleteImgBlogBridgeTable(BlogPost id);

       void EditImageOnBlogPost(Img imgToEdit);

       void EditImgBlogBridgeTable(BlogPost id);

       void AddTagIntoBlogPost(Tag tagToAdd);

       void EditTagFromBlogPost(Tag tagToEdit);

       void DeleteTagFromBlogPost(Tag tagToDelete);

       void InsertTagBlogBridgeTable(BlogPost id);

       void EditTagBlogBridgeTable(BlogPost id);

       void DeleteTagBlogBridgeTable(BlogPost id);
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.BLL.Managers;
using ToonSaloon.Data;
using ToonSaloon.Data.Factories;
using ToonSaloon.Data.InMemRepo;
using ToonSaloon.Models;

namespace ToonSaloon.BLL
{
    public class PostManager
    {
        public BlogPost GetPostByID(int id)
        {
           PostResponse response = new PostResponse();

            var repo = BlogFactory.CreatBlogPostRepository();
            var post = repo.GetPostByID(id);
       

            if (post != null)
            {
                response.Success = true;
                response.Message = "It worked!";
                response.BlogPost = post;
            }
            else
            {
                response.Success = false;
                response.Message = "Post not found!";
            }
            return post;
        }

        public List<BlogPost> GetAllPosts()
        {
            var repo = BlogFactory.CreatBlogPostRepository();
            var post = repo.GetAllPosts();
            return post;
        }

        public void AddBlogPost(BlogPost postToAdd)
        {
            var repo = BlogFactory.CreatBlogPostRepository();
            var taglist = new List<Tag>();
            var posttags = postToAdd.TagPlaceHolder;
            var manager = new TagManager();
            taglist = manager.addTagToPost(posttags);
            postToAdd.Tags = taglist;

            foreach(var img in postToAdd.Imgs)
            {
                AddImage(img);
            }
            AddImageFromBridge(postToAdd);
            AddTagFromBridge(postToAdd);
         
            repo.AddBlogPost(postToAdd); 
        }

        public void RemoveBlogPost(BlogPost postToRemove)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            DeleteImageFromBridge(postToRemove);
            foreach(var img in postToRemove.Imgs)
            {
                DeleteImage(img);
            }

            DeleteTagFromBridge(postToRemove);

            repo.RemoveBlogPost(postToRemove);
        }

        public void EditBlogPost(BlogPost postToEdit)
        {
            var taglist = new List<Tag>();
            var posttags = postToEdit.TagPlaceHolder;
            var manager = new TagManager();
            var repo = BlogFactory.CreatBlogPostRepository();

            // edit tags
            //EditTagFromBridge(postToEdit);
            DeleteTagFromBridge(postToEdit);
            taglist = manager.addTagToPost(posttags);
            postToEdit.Tags = taglist;

            // edit images
            DeleteImageFromBridge(postToEdit);
            foreach (var img in postToEdit.Imgs)
            {
                AddImage(img);
            }

            repo.EditBlogPost(postToEdit);

        }

        public void AddImage(Img imgToAdd)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.AddImageToBlogPost(imgToAdd);
        }

        public void AddImageFromBridge(BlogPost id)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.InsertImgBlogBridgeTable(id);
        }

        public void DeleteImage(Img imgToDelete)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.RemoveImageToBlogPost(imgToDelete);
        }

        public void DeleteImageFromBridge(BlogPost id)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.DeleteImgBlogBridgeTable(id);
        }

        public void EditImage(Img imgToEdit)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.EditImageOnBlogPost(imgToEdit);
        }

        public void EditImageOnBridge(BlogPost id)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.EditImgBlogBridgeTable(id);
        }

        public void AddTagFromBridge(BlogPost id)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.InsertTagBlogBridgeTable(id);
        }

        public void DeleteTagFromBridge(BlogPost id)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.DeleteTagBlogBridgeTable(id);
        }

        public void EditTagFromBridge(BlogPost id)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.EditTagBlogBridgeTable(id);
        }

        public List<BlogPost> GetPostByTag(string TagName)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            return repo.GetPostByTag(TagName);

        }

        public List<BlogPost> GetUnapprovedBlogPosts()
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            var allPosts = repo.GetAllPosts();

            var results = from p in allPosts
                where p.Approved == Approved.Waiting
                select p;

            return results.ToList();
        }
    }
}

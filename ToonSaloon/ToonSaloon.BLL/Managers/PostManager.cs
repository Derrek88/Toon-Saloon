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
            taglist = manager.addTagToPost(posttags, postToAdd.Id);
            postToAdd.Tags = taglist;
            repo.AddBlogPost(postToAdd);

            foreach (var img in postToAdd.Imgs)
            {
                if (img.Source != null)
                {
                    AddImage(img, postToAdd.Id);
                    //AddImageFromBridge(postToAdd.Id, img.Id);
                }
            }

            AddTagFromBridge(postToAdd);
        }

        public void RemoveBlogPost(BlogPost postToRemove)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            DeleteImageFromBridge(postToRemove);
            if (postToRemove.Imgs != null)
            {
                foreach (var img in postToRemove.Imgs)
                {
                    DeleteImage(img);
                }

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
            DeleteTagFromBridge(postToEdit);
            taglist = manager.addTagToPost(posttags, postToEdit.Id);
            postToEdit.Tags = taglist;
            AddTagFromBridge(postToEdit);


            // edit images
            repo.EditBlogPost(postToEdit);

        }

        public void AddImage(Img imgToAdd, int blogId)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.AddImageToBlogPost(imgToAdd, blogId);
        }

        public void AddImageFromBridge(int imgId, int newBlogId)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.InsertImgBlogBridgeTable(imgId, newBlogId);
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

        public void AddTagFromBridge(BlogPost id)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            foreach (var tag in id.Tags)
            {
               repo.InsertTagBlogBridgeTable(tag.Id, id.Id);
            }
            
        }

        public void DeleteTagFromBridge(BlogPost id)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.DeleteTagBlogBridgeTable(id);
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

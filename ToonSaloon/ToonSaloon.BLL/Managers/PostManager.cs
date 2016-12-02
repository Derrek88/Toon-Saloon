using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            repo.AddBlogPost(postToAdd);
        }

        public void RemoveBlogPost(BlogPost postToRemove)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            repo.RemoveBlogPost(postToRemove);
        }

        public void EditBlogPost(BlogPost postToEdit)
        { 

            var repo = BlogFactory.CreatBlogPostRepository();

            repo.EditBlogPost(postToEdit);

        }

        public List<BlogPost> GetPostByTag(string TagName)
        {
            var repo = BlogFactory.CreatBlogPostRepository();

            return repo.GetPostByTag(TagName);

        }
    }
}

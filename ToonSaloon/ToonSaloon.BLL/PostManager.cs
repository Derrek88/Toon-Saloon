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
    public class PostManager : IBlogPostRepository
    {
        private IBlogPostRepository _blog;

        public PostManager()
        {
            _blog = BlogFactory.CreateBlogPostRepository();
        }

        public BlogPost GetPostByID(int id)
        {
           PostResponse response = new PostResponse();
            var post = _blog.GetPostByID(id);

            if (post != null)
            {
                response.Success = true;
                response.Message = "It worked!";
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
           PostResponse response = new PostResponse();
            var post = _blog.GetAllPosts();

            if (post != null)
            {
                response.Success = true;
                response.Message = "Posts found!";
            }
            else
            {
                response.Success = false;
                response.Message = "No posts found!";
            }
            return post;
        }

        public PostResponse AddBlogPost(BlogPost postToAdd)
        {
            PostResponse response = new PostResponse();
            var post = _blog.AddBlogPost(postToAdd);

            if (post != null)
            {
                response.Success = true;
                response.Message = "Post Added!";
            }
            else
            {
                response.Success = false;
                response.Message = "Post wasn't added!";
            }
            return post;
        }

        public PostResponse RemoveBlogPost(BlogPost postToRemove)
        {

            PostResponse response = new PostResponse();
            var post = _blog.RemoveBlogPost(postToRemove);

            if (post != null)
            {
                response.Success = true;
                response.Message = "Post Removed!";
            }
            else
            {
                response.Success = false;
                response.Message = "Post wasn't removed!";
            }
            return post;
        }

        public PostResponse EditBlogPost(BlogPost postToEdit)
        {

            PostResponse response = new PostResponse();
            var post = _blog.EditBlogPost(postToEdit);

            if (post != null)
            {
                response.Success = true;
                response.Message = "Post Edited!";
            }
            else
            {
                response.Success = false;
                response.Message = "Post wasn't edited!";
            }
            return post;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyMCE.Models;

namespace TinyMCE.repo
{
    public class BlogPostRepo : IBlogPostRepo
    {
        public static List<BlogPost> _posts = new List<BlogPost>();

        public BlogPostRepo()
        {
            if (!_posts.Any())
            {
                _posts = new List<BlogPost>
                {
                    new BlogPost()
                    {
                        PostId = 1,
                        PostBody = "<p>Here is some text</p> <p><strong>Here is some bold text</strong></p> <ul><li>Things</li><li>and</li><li>stuff</li>"
                    },

                    new BlogPost()
                    {
                        PostId = 2,
                        PostBody = ""
                    }
                };
            }
        }


        public void AddBlogPost(BlogPost newBlogPost)
        {
            newBlogPost.PostId = _posts.Max(p => p.PostId) + 1;
            _posts.Add(newBlogPost);
        }

        public void DeleteBlogPost(int PostId)
        {
            throw new NotImplementedException();
        }

        public void EditBlogPost(BlogPost editedBlogPost)
        {
            throw new NotImplementedException();
        }

        public List<BlogPost> GetAllBlogPosts()
        {
            return _posts;
        }

        public BlogPost GetBlogPostById(int PostId)
        {
            throw new NotImplementedException();
        }
    }
}
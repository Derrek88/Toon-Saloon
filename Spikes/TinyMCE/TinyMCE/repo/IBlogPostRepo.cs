using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyMCE.Models;

namespace TinyMCE.repo
{
    public interface IBlogPostRepo
    {
        List<BlogPost> GetAllBlogPosts();
        BlogPost GetBlogPostById(int PostId);
        void AddBlogPost(BlogPost newBlogPost);
        void DeleteBlogPost(int PostId);
        void EditBlogPost(BlogPost editedBlogPost);
    }
}

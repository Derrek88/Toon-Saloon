using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Models;

namespace ToonSaloon.Data
{
   public class BlogDBRepo : IBlogPostRepository
   {
       private readonly string _connectiionString =
           ConfigurationManager.ConnectionStrings["ToonSaloon"].ConnectionString;

       public BlogPost GetPostByID(int id)
       {
           var repo = new BlogDBRepo();
           var post = repo.GetAllPosts();
           return post.FirstOrDefault(p => p.Id == id);
        }

       public List<BlogPost> GetAllPosts()
       {
            List<BlogPost> posts = new List<BlogPost>();

            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT * FROM BlogPost";

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ToonSaloon.Models.BlogPost newBlogPost = ConvertReaderToPost(dr);

                        posts.Add(newBlogPost);
                    }
                }
            }
            return posts;
        }

       private BlogPost ConvertReaderToPost(SqlDataReader dr)
       {
           ToonSaloon.Models.BlogPost newBlogPost = new ToonSaloon.Models.BlogPost
           {
               Id = (int) dr["BlogId"],
               Body = dr["Body"].ToString(),
               AuthorName = dr["AuthorName"].ToString(),
               Category = (Category) dr["Category"],
               Approved = (Enum) dr["isApproved"],
               DateCreated = (DateTime) dr["DateCreated"],
               Headline = dr["Headlines"].ToString(),
               Subtitle = dr["Subtitle"].ToString(),
               
               //Tags = dr["Tags"].ToString()
               //Imgs = dr["Imgs"].ToString()
               //Youtubes = dr["Youtube"].ToString(),

           };
            return newBlogPost;
       }

       public void AddBlogPost(BlogPost postToAdd)
       {
           throw new NotImplementedException();
       }

       public void RemoveBlogPost(BlogPost postToRemove)
       {
           throw new NotImplementedException();
       }

       public void EditBlogPost(BlogPost postToEdit)
       {
           throw new NotImplementedException();
       }
    }
}

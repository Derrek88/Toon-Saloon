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
           var post = repo.GetPostByID(id);
           return post;
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
               Approved = (Approved) dr["Approved"],
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
           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();
               cmd.Connection = cn;
               cmd.CommandText =
                   @"INSERT INTO BlogPost(Body, AuthorName, Category, Approved, DateCreated, Headline, Subtitle) 
                                    VALUE (@Body, @AuthorName, @Category, @Approved, @DateCreated, @Headline, @Subtitle";

               cmd.Parameters.AddWithValue("@Body", postToAdd.Body);
               cmd.Parameters.AddWithValue("@AuthorName", postToAdd.AuthorName);
               cmd.Parameters.AddWithValue("@Category", postToAdd.Category);
               cmd.Parameters.AddWithValue("@Approved", postToAdd.Approved);
               cmd.Parameters.AddWithValue("@DateCreated", postToAdd.DateCreated);
               cmd.Parameters.AddWithValue("@Headline", postToAdd.Headline);
               cmd.Parameters.AddWithValue("@Subtitle", postToAdd.Subtitle);

               cn.Open();

               cmd.ExecuteNonQuery();

           }
       }

       public void RemoveBlogPost(BlogPost postToRemove)
       {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    @"DELETE FROM BlogPost 
                              WHERE BlogId = @BlogId";

                cmd.Parameters.AddWithValue("@Body", postToRemove.Body);
                cmd.Parameters.AddWithValue("@AuthorName", postToRemove.AuthorName);
                cmd.Parameters.AddWithValue("@Category", postToRemove.Category);
                cmd.Parameters.AddWithValue("@Approved", postToRemove.Approved);
                cmd.Parameters.AddWithValue("@DateCreated", postToRemove.DateCreated);
                cmd.Parameters.AddWithValue("@Headline", postToRemove.Headline);
                cmd.Parameters.AddWithValue("@Subtitle", postToRemove.Subtitle);

                cn.Open();

                cmd.ExecuteNonQuery();

            }
        }

       public void EditBlogPost(BlogPost postToEdit)
       {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    @"UPDATE BlogPost
                        SET Body = @Body, AuthorName = @AuthorName, @Cateogry = Cateogry, Approved = @Approved, DateCreated = @DateCreated, Headline = @Headline, Subtitle = @Subtitle
                        WHERE BlogId = @BlogId";

                cmd.Parameters.AddWithValue("@Body", postToEdit.Body);
                cmd.Parameters.AddWithValue("@AuthorName", postToEdit.AuthorName);
                cmd.Parameters.AddWithValue("@Category", postToEdit.Category);
                cmd.Parameters.AddWithValue("@Approved", postToEdit.Approved);
                cmd.Parameters.AddWithValue("@DateCreated", postToEdit.DateCreated);
                cmd.Parameters.AddWithValue("@Headline", postToEdit.Headline);
                cmd.Parameters.AddWithValue("@Subtitle", postToEdit.Subtitle);

                cn.Open();

                cmd.ExecuteNonQuery();

            }
        }

       public List<BlogPost> GetPostByTag(string TagName)
       {
           throw new NotImplementedException();
       }
   }
}

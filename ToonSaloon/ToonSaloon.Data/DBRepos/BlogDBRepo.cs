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
                        BlogPost post = posts.FirstOrDefault(p => p.Id == (int) dr["BlogId"]);

                        if (post == null)
                        {
                            post = ConvertReaderToPost(dr);
                            posts.Add(post);
                        }

                        //ToonSaloon.Models.BlogPost newBlogPost = ConvertReaderToPost(dr);

                        post.Tags = GetTagsByPostId(post.Id);
                        post.Imgs = GetImgsByPostId(post.Imgs);
                        post.Youtubes = GetYoutubeById(post.Youtubes);

                    }
                }
            }
            return posts;
        }

       private List<Youtube> GetYoutubeById(List<Youtube> id)
       {
           List<Youtube> youtubes = new List<Youtube>();

           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.CommandText = @"SELECT YoutubeId, TubeId, Description
                                        FROM Youtube y
                                           JOIN Youtube_BlogBride b
                                               ON y.YoutubeId = b.YoutubeId
                                                    WHERE y.Blog.Id = @Blog.Id";

                cmd.Parameters.AddWithValue("@Blog.Id", id);

                cmd.Connection = cn;

                cn.Open();

               using (var dr = cmd.ExecuteReader())
               {
                   while (dr.Read())
                   {
                       Youtube youtube = ConvertToYoutube(dr);

                       youtubes.Add(youtube);
                   }
               }
            }
           return youtubes;

       }

       private Youtube ConvertToYoutube(SqlDataReader dr)
       {
           return new Youtube()
           {
               Id = (int) dr["YoutubeId"],
               Description = dr["Description"].ToString(),
               TubeId = dr["TubeId"].ToString()

           };
       }

       private List<Img> GetImgsByPostId(List<Img> id)
       {
           List<Img> imgs = new List<Img>();

           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.CommandText = @"ImgId, Name
                                        FROM Img i
                                           JOIN Img_BlogBride b
                                             ON i.ImgId = b.ImgId
                                               WHERE b.BlogId = @Blog.Id";

               cmd.Parameters.AddWithValue("@Blog.Id", id);

               cmd.Connection = cn;

                cn.Open();

               using (var dr = cmd.ExecuteReader())
               {
                   while (dr.Read())
                   {
                       Img img = ConvertToImg(dr);

                        imgs.Add(img);
                   }
               }
           }
           return imgs;
       }

       private List<Tag> GetTagsByPostId(int id)
       {
           List<Tag> tags = new List<Tag>();

           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.CommandText = @"SELECT TagId, Name
                                   From Tag t
                                        JOIN Tag_BlogBridge b
                                            ON t.TagId = b.TagId
                                                WHERE b.BlogId = @Blog.Id";

               cmd.Parameters.AddWithValue("@Blog.Id", id);

               cmd.Connection = cn;

               cn.Open();

               using (var dr = cmd.ExecuteReader())
               {
                   while (dr.Read())
                   {
                       Tag tag = ConvertToTag(dr);

                       tags.Add(tag);
                   }
               }
           }
           return tags;
       }

       private Tag ConvertToTag(SqlDataReader dr)
       {
           return new Tag()
           {
               Id = (int) dr["TagId"],
               Name = dr["Name"].ToString()
           };
       }

       private Img ConvertToImg(SqlDataReader dr)
       {
           return new Img()
           {
               Id = (int) dr["ImgId"],
               Title = dr["Title"].ToString(),
               Source = dr["Source"].ToString(),
               Description = dr["Description"].ToString()
           };
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

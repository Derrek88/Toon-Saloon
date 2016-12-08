using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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
           var allPosts = GetAllPosts();
           var blogPost = allPosts.FirstOrDefault(b => b.Id == id);
           return blogPost;
       }

       public List<Tag> GetTop10Tags()
       {
           List<Tag> tags = new List<Tag>();

           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();
               cmd.Connection = cn;
               cmd.CommandText = @"SELECT TagName
                                      FROM Tag
                                        JOIN Blog_TagBridge on TagId
                                            GROUPBY Name
                                                ORDERBY Count Desc
                                                    LIMIT 10";
               cmd.Connection = cn;

               cn.Open();

               using (var dr = cmd.ExecuteReader())
               {
                   while (dr.Read())
                   {
                       Tag tag = tags.FirstOrDefault(t => t.Id == (int) dr["TagId"]);

                       if (tag == null)
                       {
                           tag = ConvertToTag(dr);
                           tags.Add(tag);
                       }
                   }
               }
           }
           return tags;
       }

       public List<BlogPost> GetAllPosts()
       {
            List<BlogPost> posts = new List<BlogPost>();

            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT * FROM BlogPost";

                cmd.Connection = cn;

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

                        post.Tags = GetTagsByPostId(post.Id);
                        post.Imgs = GetImgsByPostId(post.Id);

                    }
                }
            }
            return posts;
        }

       private List<Img> GetImgsByPostId(int id)
        {
            List<Img> imgs = new List<Img>();

            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();

                cmd.CommandText = @"SELECT i.ImgId, i.Title, i.Description, i.Source
                                        FROM Img i
                                           JOIN Img_BlogBridge b
                                             ON i.ImgId = b.ImgId
                                        WHERE b.BlogId = @BlogId";

                cmd.Parameters.AddWithValue("@BlogId", id);

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

                cmd.CommandText = @"SELECT t.TagId, t.Name
                                   From Tag t
                                        JOIN Tag_BlogBridge b
                                            ON t.TagId = b.TagId
                                                WHERE b.BlogId = @BlogId";

                cmd.Parameters.AddWithValue("@BlogId", id);


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
                Id = (int)dr["TagId"],
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
               Headline = dr["Headline"].ToString(),
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
                   @"dbo.AddPost";
               cmd.CommandType = CommandType.StoredProcedure;
               SqlParameter param = new SqlParameter()
               {
                   SqlDbType = SqlDbType.Int,
                   ParameterName = @"BlogId",
                   SourceColumn = "BlogId",
                   Direction = ParameterDirection.Output
               };

               cmd.Parameters.Add(param);
               cmd.Parameters.AddWithValue("@Body", postToAdd.Body);
               cmd.Parameters.AddWithValue("@AuthorName", postToAdd.AuthorName);
               cmd.Parameters.AddWithValue("@Cateogry", postToAdd.Category);
               cmd.Parameters.AddWithValue("@Approved", postToAdd.Approved);
               cmd.Parameters.AddWithValue("@DateCreated", postToAdd.DateCreated);
               cmd.Parameters.AddWithValue("@Headline", postToAdd.Headline);
               cmd.Parameters.AddWithValue("@Subtitle", postToAdd.Subtitle);

               cn.Open();

               cmd.ExecuteNonQuery();
               postToAdd.Id = int.Parse(param.Value.ToString());

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

                cmd.Parameters.AddWithValue("@BlogId", postToRemove.Id);

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
                        SET Body = @Body, AuthorName = @AuthorName, @Category = Category, Approved = @Approved, DateCreated = @DateCreated, Headline = @Headline, Subtitle = @Subtitle
                        WHERE BlogId = @BlogId";

                cmd.Parameters.AddWithValue("@BlogId", postToEdit.Id);
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

       public void AddImageToBlogPost(Img imgToAdd, int blogid)
       {

           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.Connection = cn;
               cmd.CommandText = @"dbo.AddImage";
               cmd.CommandType = CommandType.StoredProcedure;
               SqlParameter param = new SqlParameter()
               {
                   SqlDbType = SqlDbType.Int,
                   ParameterName = @"ImgId",
                   SourceColumn = "ImgId",
                   Direction = ParameterDirection.Output
               };

               cmd.Parameters.Add(param);
               cmd.Parameters.AddWithValue("@Title", imgToAdd.Title);
               cmd.Parameters.AddWithValue("@Source", imgToAdd.Source);
               cmd.Parameters.AddWithValue("@Description", imgToAdd.Description);

               cn.Open();

               cmd.ExecuteNonQuery();
               imgToAdd.Id = int.Parse(param.Value.ToString());
               InsertImgBlogBridgeTable(imgToAdd.Id, blogid);


           }
       }

       public void RemoveImageToBlogPost(Img imgToDelete)
       {
           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();
               cmd.Connection = cn;
               cmd.CommandText = @"DELETE FROM Img
                                         WHERE ImgId = @ImgId";

               cmd.Parameters.AddWithValue("@ImgId", imgToDelete.Id);

               cn.Open();

               cmd.ExecuteNonQuery();
           }
       }

       public void InsertTagBlogBridgeTable(int tagId, int newBlogId )
       {


           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.Connection = cn;
               cmd.CommandText = @"INSERT INTO Tag_BlogBridge (TagId, BlogId)
                                                VALUES (@TagId, @BlogId);";

               cmd.Parameters.AddWithValue("@BlogId", newBlogId);
               cmd.Parameters.AddWithValue("@TagId", tagId);

               cn.Open();

               cmd.ExecuteNonQuery();
           }

       }

       public void DeleteTagBlogBridgeTable(BlogPost id)
       {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();

                cmd.Connection = cn;
                cmd.CommandText = @"DELETE FROM Tag_BlogBridge
                                                WHERE BlogId = @BlogId";

                cmd.Parameters.AddWithValue("@BlogId", id.Id);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

       public void DeleteImgBlogBridgeTable(BlogPost id)
       {

           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.Connection = cn;
               cmd.CommandText = @"DELETE FROM Img_BlogBridge
                                                WHERE BlogId = @BlogId";

               cmd.Parameters.AddWithValue("@BlogId", id.Id);

               cn.Open();

               cmd.ExecuteNonQuery();
           }

       }

       public void InsertImgBlogBridgeTable(int imgId, int newBlogId)
       {
           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.Connection = cn;
               cmd.CommandText = @"INSERT INTO Img_BlogBridge (BlogId, ImgId)
                                                VALUES (@Blogid, @ImgId)";

               cmd.Parameters.AddWithValue("@ImgId", imgId);
               cmd.Parameters.AddWithValue("@BlogId", newBlogId);
               cn.Open();
               cmd.ExecuteNonQuery();
           }
       }

       
   }
}

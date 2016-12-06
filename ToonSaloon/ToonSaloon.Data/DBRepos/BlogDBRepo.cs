using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

                        //ToonSaloon.Models.BlogPost newBlogPost = ConvertReaderToPost(dr);

                        post.Tags = GetTagsByPostId(post.Id);
                        post.Imgs = GetImgsByPostId(post.Id);
                        post.Youtubes = GetYoutubeById(post.Id);

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

       private List<Youtube> GetYoutubeById(int id)
       {
           List<Youtube> youtubes = new List<Youtube>();

           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.CommandText = @"SELECT y.YoutubeId, y.TubeId, y.Description
                                        FROM Youtube y
                                           JOIN Youtube_BlogBridge b
                                               ON y.YoutubeId = b.YoutubeId
                                                    WHERE b.BlogId = @BlogId";

                cmd.Parameters.AddWithValue("@BlogId", id);

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

       private Youtube ConvertToYoutube(SqlDataReader dr)
       {
           return new Youtube()
           {
               Id = (int) dr["YoutubeId"],
               Description = dr["Description"].ToString(),
               TubeId = dr["TubeId"].ToString()

           };
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
                   @"INSERT INTO BlogPost(Body, AuthorName, Category, Approved, DateCreated, Headline, Subtitle) 
                                    VALUES (@Body, @AuthorName, @Category, @Approved, @DateCreated, @Headline, @Subtitle)";

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

       public void AddImageToBlogPost(Img imgToAdd)
       {

           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.Connection = cn;
               cmd.CommandText = @"INSERT INTO Img(Title, Source, Description)
                                                VALUES (@Title, @Source, @Description)";

               cmd.Parameters.AddWithValue("@Title", imgToAdd.Title);
               cmd.Parameters.AddWithValue("@Source", imgToAdd.Source);
               cmd.Parameters.AddWithValue("@Description", imgToAdd.Description);

               cn.Open();

               cmd.ExecuteNonQuery();
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

               cmd.Parameters.AddWithValue("@Title", imgToDelete.Title);
               cmd.Parameters.AddWithValue("@Source", imgToDelete.Source);
               cmd.Parameters.AddWithValue("@Description", imgToDelete.Description);

               cn.Open();

               cmd.ExecuteNonQuery();
           }
       }

       public void EditImageOnBlogPost(Img imgToEdit)
       {
           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();
               cmd.Connection = cn;
               cmd.CommandText = @"UPDATE Img
                                       SET Title = @Title, Source = @Source, Description = @Description
                                            WHERE ImgId = @ImgId";

               cmd.Parameters.AddWithValue("@Title", imgToEdit.Title);
               cmd.Parameters.AddWithValue("@Source", imgToEdit.Source);
               cmd.Parameters.AddWithValue("@Description", imgToEdit.Description);

               cn.Open();

               cmd.ExecuteNonQuery();
           }
       }

       public void AddTagIntoBlogPost(Tag tagToAdd)
        {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();

                cmd.Connection = cn;
                cmd.CommandText = @"INSERT INTO Tag(Name)
                                            VALUES (@Name)";

                cmd.Parameters.AddWithValue("@Name", tagToAdd.Name);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

       public void EditTagFromBlogPost(Tag tagToEdit)
       {
           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();
               cmd.Connection = cn;
               cmd.CommandText = @"UPDATE Tag
                                        SET Name = @Name";

               cmd.Parameters.AddWithValue("@Name", tagToEdit.Name);

               cn.Open();

               cmd.ExecuteNonQuery();
           }
       }

       public void DeleteTagFromBlogPost(Tag tagToDelete)
       {
           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();
               cmd.Connection = cn;
               cmd.CommandText = @"DELETE FROM Tag
                                            WHERE TagId = @TagId";

               cmd.Parameters.AddWithValue("@Name", tagToDelete.Name);

               cn.Open();

               cmd.ExecuteNonQuery();
           }
       }

       public void InsertTagBlogBridgeTable(BlogPost id)
       {
           foreach (var tag in id.Tags)
           {
               using (var cn = new SqlConnection(_connectiionString))
               {
                   var cmd = new SqlCommand();

                   cmd.Connection = cn;
                   cmd.CommandText = @"INSTER INTO Tag_BlogBridge (TagId, BlogId)
                                                VALUES (@TagId, @BlogId)";

                    cmd.Parameters.AddWithValue("@BlogId", id);
                    cmd.Parameters.AddWithValue("@TagId", tag.Id);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
           }
       }

       public void EditTagBlogBridgeTable(BlogPost id)
       {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();

                cmd.Connection = cn;
                cmd.CommandText = @"UPDATE Tag_BlogBridge
                                        WHERE BlogId = @BlogId;";

                cmd.Parameters.AddWithValue("@BlogId", id);

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

                cmd.Parameters.AddWithValue("@BlogId", id);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

       public void EditImgBlogBridgeTable(BlogPost id)
       {
           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();

               cmd.Connection = cn;
               cmd.CommandText = @"UPDATE Img_BlogBridge
                                        WHERE BlogId = @BlogId";

               cmd.Parameters.AddWithValue("@BlogId", id);

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

               cmd.Parameters.AddWithValue("@BlogId", id);

               cn.Open();

               cmd.ExecuteNonQuery();
           }

       }

       public void InsertImgBlogBridgeTable(BlogPost id)
       {
           foreach (var image in id.Imgs )
           {
               using (var cn = new SqlConnection(_connectiionString))
               {
                   var cmd = new SqlCommand();

                   cmd.Connection = cn;
                   cmd.CommandText = @"INSERT INTO Img_BlogBridge (BlogId, ImgId)
                                                VALUES (@BlogId, @ImgId)";

                   cmd.Parameters.AddWithValue("@BlogId", id);
                   cmd.Parameters.AddWithValue("@ImgId", image.Id);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }

            }
       }

       public List<BlogPost> GetPostByTag(string TagName)
       {
           throw new NotImplementedException();
       }
   }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.Interface;
using ToonSaloon.Models;

namespace ToonSaloon.Data.DBRepos
{
    public class StaticDBRepo : IStaticRepository
    {
        private readonly string _connectiionString =
            ConfigurationManager.ConnectionStrings["ToonSaloon"].ConnectionString;

        public StaticPage GetPageByID(int id)
        {
            var allPages = GetAllPages();
            var page = allPages.FirstOrDefault(p => p.Id == id);
            return page;
        }

        public List<StaticPage> GetAllPages()
        {
            List<StaticPage> pages = new List<StaticPage>();

            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();

                cmd.CommandText = @"SELECT *
                                        FROM StaticPage";
                cmd.Connection = cn;

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        StaticPage page = pages.FirstOrDefault(p => p.Id == (int) dr["StaticId"]);

                        //ToonSaloon.Models.StaticPage newPage = ConvertReaderToPage(dr);

                        if (page == null)
                        {
                            page = ConvertReaderToPage(dr);
                            pages.Add(page);
                        }

                        //Tag tag = ConvertToTag(dr);

                        //page.Tag.Add(tag);
                       page.Tag = GetTagsByPageId(page.Id);
                    }
                }
            }
            return pages;
        }

        private Tag ConvertToTag(SqlDataReader dr)
        {
            return new Tag()
            {
                Id = (int) dr["TagId"],
                Name = dr["Name"].ToString()
            };
        }

        private StaticPage ConvertReaderToPage(SqlDataReader dr)
        {
            ToonSaloon.Models.StaticPage newPage = new ToonSaloon.Models.StaticPage
            {
                Id = (int) dr["StaticId"],
                Name = dr["Name"].ToString(),
                Body = dr["Body"].ToString(),
                DateCreated = (DateTime) dr["DateCreated"],
                Approved = (Approved) dr["Approved"],
                Category = (Category) dr["Category"]
               
            };
           return newPage;
       }

        public void AddStaticPage(StaticPage pageToAdd)
        {
           using (var cn = new SqlConnection(_connectiionString))
           {
               var cmd = new SqlCommand();
               cmd.Connection = cn;
               cmd.CommandText =
                   @"dbo.AddPage";
               cmd.CommandType = CommandType.StoredProcedure;

               SqlParameter param = new SqlParameter()
               {
                   SqlDbType = SqlDbType.Int,
                   ParameterName = @"StaticId",
                   SourceColumn = "StaticId",
                   Direction = ParameterDirection.Output
               };
               cmd.Parameters.Add(param);
               cmd.Parameters.AddWithValue("@Name", pageToAdd.Name);
               cmd.Parameters.AddWithValue("@Body", pageToAdd.Body);
               cmd.Parameters.AddWithValue("@DateCreated", pageToAdd.DateCreated);
               cmd.Parameters.AddWithValue("@Approved", pageToAdd.Approved);
               cmd.Parameters.AddWithValue("@Category", pageToAdd.Category);

               cn.Open();

               cmd.ExecuteNonQuery();
               pageToAdd.Id = int.Parse(param.Value.ToString());
           }
       }

       public void RemoveStaticPage(StaticPage pageToRemove)
       {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    @"DELETE FROM StaticPage
                                WHERE StaticId = @StaticId";
                cmd.Parameters.AddWithValue("@StaticId", pageToRemove.Id);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

       public void EditStaticPage(StaticPage pageToEdit)
       {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    @"UPDATE StaticPage
                            SET Name = @Name, Body = @Body, DateCreated = @DateCreated, Approved = @Approved
                             WHERE StaticId = @StaticId";

                cmd.Parameters.AddWithValue("@StaticId", pageToEdit.Id);
                cmd.Parameters.AddWithValue("@Name", pageToEdit.Name);
                cmd.Parameters.AddWithValue("@Body", pageToEdit.Body);
                cmd.Parameters.AddWithValue("@DateCreated", pageToEdit.DateCreated);
                cmd.Parameters.AddWithValue("@Approved", pageToEdit.Approved);
                cmd.Parameters.AddWithValue("@Category", pageToEdit.Category);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

       public void InsertTagStaticBridgeTable(int tagId, int pageId)
        {
           
                using (var cn = new SqlConnection(_connectiionString))
                {
                    var cmd = new SqlCommand();

                    cmd.Connection = cn;
                    cmd.CommandText = @"INSERT INTO Page_TagBridge (TagId, StaticId)
                                                VALUES (@TagId, @StaticId)";

                    cmd.Parameters.AddWithValue("@TagId", tagId);
                    cmd.Parameters.AddWithValue("@StaticId", pageId);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            
        }

       public void DeleteTagStaticBridgeTable(StaticPage id)
        {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();

                cmd.Connection = cn;
                cmd.CommandText = @"DELETE FROM Page_TagBridge
                                               WHERE StaticId = @StaticId";
                                               

                cmd.Parameters.AddWithValue("@StaticId", id.Id);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

       public void EditTagStaticBridgeTable(StaticPage id)
        {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();

                cmd.Connection = cn;
                cmd.CommandText = @"UPDATE Page_TagBridge
                                        WHERE PageId = @PageId";

                cmd.Parameters.AddWithValue("@PageId", id);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

       private List<Tag> GetTagsByPageId(int id)
        {
            List<Tag> tags = new List<Tag>();

            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();

                cmd.CommandText = @"SELECT t.TagId, t.Name
                                        FROM Tag t
                                            JOIN Page_TagBridge b
                                                ON t.TagId = b.TagId
                                                  WHERE b.StaticId = @StaticId";

                cmd.Parameters.AddWithValue("@StaticId", id);

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
    }
}

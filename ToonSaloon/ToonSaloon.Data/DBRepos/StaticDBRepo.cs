using System;
using System.Collections.Generic;
using System.Configuration;
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
                Id = (int) dr["PageId"],
                Name = dr["Name"].ToString(),
                Body = dr["Body"].ToString(),
                DateCreated = (DateTime) dr["DateCreated"],
                Approved = (Approved) dr["Approved"],
               
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
                   @"INSERT INTO CartoonOfTheDay(Name, Body, DateCreated, Approced)
                            VALUES (@Name, @Body, @DateCreated, @Approved)";
               cmd.Parameters.AddWithValue("@Name", pageToAdd.Name);
               cmd.Parameters.AddWithValue("@Body", pageToAdd.Body);
               cmd.Parameters.AddWithValue("@DateCreated", pageToAdd.DateCreated);
               cmd.Parameters.AddWithValue("@Approved", pageToAdd.Approved);

               cn.Open();

               cmd.ExecuteNonQuery();
           }
       }

       public void RemoveStaticPage(StaticPage pageToRemove)
       {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    @"DELETE FROM CartoonOfTheDay
                                WHERE CotDId = @CotDId";
                cmd.Parameters.AddWithValue("@Name", pageToRemove.Name);
                cmd.Parameters.AddWithValue("@Body", pageToRemove.Body);
                cmd.Parameters.AddWithValue("@DateCreated", pageToRemove.DateCreated);
                cmd.Parameters.AddWithValue("@Approved", pageToRemove.Approved);

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
                    @"UPDATE CartoonOfTheDay
                            SET Name = @Name, Body = @Body, DateCreated = @DateCreated, Approved = @Approved
                             WHERE CotDId = @CotDId";
                cmd.Parameters.AddWithValue("@Name", pageToEdit.Name);
                cmd.Parameters.AddWithValue("@Body", pageToEdit.Body);
                cmd.Parameters.AddWithValue("@DateCreated", pageToEdit.DateCreated);
                cmd.Parameters.AddWithValue("@Approved", pageToEdit.Approved);

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

                cmd.CommandText = @"SELECT TagId, Name
                                        FROM Tag t
                                            JOIN Page_TagBridge b
                                                ON t.TagId = b.TagId
                                                  WHERE t.Page.Id = @Page.Id";

                cmd.Parameters.AddWithValue("@Page.Id", id);

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

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
   public class StaticDBRepo: IStaticRepository
    {
        private readonly string _connectiionString =
           ConfigurationManager.ConnectionStrings["ToonSaloon"].ConnectionString;

        public StaticPage GetPageByID(int id)
       {
            var repo = new StaticDBRepo();
            var page = repo.GetPageByID(id);
            return page;
        }

       public List<StaticPage> GetAllPages()
       {
            List<StaticPage> posts = new List<StaticPage>();

            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT * FROM StaticPage";

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ToonSaloon.Models.StaticPage newPage = ConvertReaderToPage(dr);

                        posts.Add(newPage);
                    }
                }
            }
            return posts;
        }

       private StaticPage ConvertReaderToPage(SqlDataReader dr)
       {
           ToonSaloon.Models.StaticPage newPage = new ToonSaloon.Models.StaticPage
           {
               Name = dr["Name"].ToString(),
               Body = dr["Body"].ToString(),
               DateCreated = (DateTime) dr["DateCreated"],
               Approved = (Approved) dr["Approved"],
               //Tag = dr["Tag"].ToString()
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
                            VALUE (@Name, @Body, @DateCreated, @Approved)";
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
    }
}

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
    public class ToonOfDayDBRepo: IToonOfDayRepository
    {
        private readonly string _connectiionString =
           ConfigurationManager.ConnectionStrings["ToonSaloon"].ConnectionString;

        public CartoonOfTheDay GetPostByID(int id)
        {
            var repo = new ToonOfDayDBRepo();
            var toon = repo.GetPostByID(id);
            return toon;
        }


        public List<CartoonOfTheDay> GetAllToons()
        {
            List<CartoonOfTheDay> toon = new List<CartoonOfTheDay>();

            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT * FROM CartoonOfTheDay";

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ToonSaloon.Models.CartoonOfTheDay newToon = ConvertReaderToToon(dr);

                        toon.Add(newToon);
                    }
                }
            }
            return toon;
        }

        private CartoonOfTheDay ConvertReaderToToon(SqlDataReader dr)
        {
            ToonSaloon.Models.CartoonOfTheDay newToon = new CartoonOfTheDay
            {
                Id = (int) dr["CotDId"],
                Author = dr["Author"].ToString(),
                ShowName = dr["ShowName"].ToString(),
                Season = (int) dr["Season"],
                Episode = (int) dr["Episode"],
                Approved = (Approved) dr["Approved"],
                DateCreated = (DateTime) dr["DateCreated"],
                ImgUrl = dr["ImgUrl"].ToString()

            };

            return newToon;
        }

        public void AddToonOfDay(CartoonOfTheDay toonToAdd)
        {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    @"INSERT INTO CartoonOfTheDay(Author, ShowName, Season, Episode, Approved, DateCreated, ImgUrl)
                                            VALUE (@Author, @ShowName, @Season, @Episode, @Approved, @DateCreared, @ImgUrl)";
                cmd.Parameters.AddWithValue("@Author", toonToAdd.Author);
                cmd.Parameters.AddWithValue("@ShowName", toonToAdd.ShowName);
                cmd.Parameters.AddWithValue("@Season", toonToAdd.Season);
                cmd.Parameters.AddWithValue("@Episode", toonToAdd.Episode);
                cmd.Parameters.AddWithValue("@Approved", toonToAdd.Approved);
                cmd.Parameters.AddWithValue("@DateCreated", toonToAdd.DateCreated);
                cmd.Parameters.AddWithValue("@ImgUrl", toonToAdd.ImgUrl);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveToonOfDay(CartoonOfTheDay toonToRemove)
        {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    @"DELETE FROM CartoonOfTheDay
                                WHERE CotDId = @CotDId";

                cmd.Parameters.AddWithValue("@Author", toonToRemove.Author);
                cmd.Parameters.AddWithValue("@ShowName", toonToRemove.ShowName);
                cmd.Parameters.AddWithValue("@Season", toonToRemove.Season);
                cmd.Parameters.AddWithValue("@Episode", toonToRemove.Episode);
                cmd.Parameters.AddWithValue("@Approved", toonToRemove.Approved);
                cmd.Parameters.AddWithValue("@DateCreated", toonToRemove.DateCreated);
                cmd.Parameters.AddWithValue("@ImgUrl", toonToRemove.ImgUrl);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void EditToonOfDay(CartoonOfTheDay toonToEdit)
        {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    @"UPDATE CartoonOfTheDay
                         SET Author = @Author, ShowName = @ShowName, Seaon = @Season, Episode = @Episode, Approve = @Approved, DateCreated = @DateCreated, ImgUrl = @ImgUrl
                          WHERE CotDId = @CotDId";

                cmd.Parameters.AddWithValue("@Author", toonToEdit.Author);
                cmd.Parameters.AddWithValue("@ShowName", toonToEdit.ShowName);
                cmd.Parameters.AddWithValue("@Season", toonToEdit.Season);
                cmd.Parameters.AddWithValue("@Episode", toonToEdit.Episode);
                cmd.Parameters.AddWithValue("@Approved", toonToEdit.Approved);
                cmd.Parameters.AddWithValue("@DateCreated", toonToEdit.DateCreated);
                cmd.Parameters.AddWithValue("@ImgUrl", toonToEdit.ImgUrl);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

    }
}

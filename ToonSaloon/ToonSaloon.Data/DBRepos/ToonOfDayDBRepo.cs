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
            var allToons = GetAllToons();
            var toon = allToons.FirstOrDefault(t => t.Id == id);
            return toon;
        }


        public List<CartoonOfTheDay> GetAllToons()
        {
            List<CartoonOfTheDay> toon = new List<CartoonOfTheDay>();

            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT * 
                                            FROM CartoonOfTheDay";

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
                WhenPosted = (DateTime) dr["WhenPosted"],
                HasNotBeenPosted = (bool) dr["HasNotBeenPosted"],
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
                    @"INSERT INTO CartoonOfTheDay(Author, ShowName, Season, Episode, Approved, DateCreated, ImgUrl, WhenPosted, HasNotBeenPosted)
                                            VALUES (@Author, @ShowName, @Season, @Episode, @Approved, @DateCreated, @ImgUrl, @WhenPosted, @HasNotBeenPosted)";

                cmd.Parameters.AddWithValue("@Author", toonToAdd.Author);
                cmd.Parameters.AddWithValue("@ShowName", toonToAdd.ShowName);
                cmd.Parameters.AddWithValue("@Season", toonToAdd.Season);
                cmd.Parameters.AddWithValue("@Episode", toonToAdd.Episode);
                cmd.Parameters.AddWithValue("@Approved", toonToAdd.Approved);
                cmd.Parameters.AddWithValue("@DateCreated", toonToAdd.DateCreated);
                cmd.Parameters.AddWithValue("@ImgUrl", toonToAdd.ImgUrl);
                cmd.Parameters.AddWithValue("@WhenPosted", toonToAdd.WhenPosted);
                cmd.Parameters.AddWithValue("@HasNotBeenPosted", toonToAdd.HasNotBeenPosted);

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

                cmd.Parameters.AddWithValue("@CotDId", toonToRemove.Id);

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
                         SET Author = @Author, ShowName = @ShowName, Season = @Season, Episode = @Episode, Approved = @Approved, DateCreated = @DateCreated, ImgUrl = @ImgUrl, WhenPosted = @WhenPosted, HasNotBeenPosted = @HasNotBeenPosted
                          WHERE CotDId = @CotDId";

                cmd.Parameters.AddWithValue("@CotDId", toonToEdit.Id);
                cmd.Parameters.AddWithValue("@Author", toonToEdit.Author);
                cmd.Parameters.AddWithValue("@ShowName", toonToEdit.ShowName);
                cmd.Parameters.AddWithValue("@Season", toonToEdit.Season);
                cmd.Parameters.AddWithValue("@Episode", toonToEdit.Episode);
                cmd.Parameters.AddWithValue("@Approved", toonToEdit.Approved);
                cmd.Parameters.AddWithValue("@DateCreated", toonToEdit.DateCreated);
                cmd.Parameters.AddWithValue("@ImgUrl", toonToEdit.ImgUrl);
                cmd.Parameters.AddWithValue("@WhenPosted", toonToEdit.WhenPosted);
                cmd.Parameters.AddWithValue("@HasNotBeenPosted", toonToEdit.HasNotBeenPosted);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

    }
}

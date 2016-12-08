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
    public class TagDBRepo : ITagRepository
    {
        private readonly string _connectiionString =
           ConfigurationManager.ConnectionStrings["ToonSaloon"].ConnectionString;

        public List<Tag> GetAllTags()
        {
            List<Tag> tags = new List<Tag>();

            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT * FROM Tag";

                cmd.Connection = cn;

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ToonSaloon.Models.Tag newTag = ConvertReaderToTag(dr);

                        tags.Add(newTag);
                    }
                }
            }
            return tags;
        }

        private Tag ConvertReaderToTag(SqlDataReader dr)
        {
            ToonSaloon.Models.Tag newTag = new ToonSaloon.Models.Tag
            {
                Id = (int) dr["TagId"],
                Name = dr["Name"].ToString()
            };
            return newTag;
        }

        public Tag GetTagById(int id)
        {
            var allTags = GetAllTags();
            var tag = allTags.FirstOrDefault(b => b.Id == id);
            return tag;
        }

        public void AddTag(Tag tagToAdd, int blogId)
        {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"dbo.AddTag";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter()
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = @"TagId",
                    SourceColumn = "TagId",
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@Name", tagToAdd.Name);

               

                cn.Open();

                cmd.ExecuteNonQuery();
                tagToAdd.Id = int.Parse(param.Value.ToString());
                //new BlogDBRepo().InsertTagBlogBridgeTable(tagToAdd.Id, blogId);

            }
        }

        public void RemoveTag(Tag tagToRemove)
        {
            using (var cn = new SqlConnection(_connectiionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"DELETE FROM Tag
                                          WHERE TagId = @TagId";
                cmd.Parameters.AddWithValue("@TagId", tagToRemove.Id);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}

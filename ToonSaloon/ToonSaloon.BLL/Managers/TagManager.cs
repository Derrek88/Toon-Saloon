using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.Factories;
using ToonSaloon.Data.Interface;
using ToonSaloon.Models;
using ToonSaloon.Models.Responses;

namespace ToonSaloon.BLL.Managers
{
    public class TagManager 
    {
        public List<Tag> GetAllTags()
        {
            var repo = TagFactory.CreateTagRepository();
            var post = repo.GetAllTags();
            return post;
        }

        public Tag GetTagById(int id)
        {
            TagResponse response = new TagResponse();

            var repo = TagFactory.CreateTagRepository();
            var tag = repo.GetTagById(id);

            if (tag != null)
            {
                response.Success = true;
                response.Message = "It worked!";
                response.Tag = tag;
            }
            else
            {
                response.Success = false;
                response.Message = "Tag not found!";
            }
            return tag;


        }

        public void AddTag(Tag tagToAdd)
        {
            var repo = TagFactory.CreateTagRepository();

            repo.AddTag(tagToAdd);
        }

        public void RemoveTag(Tag tagToRemove)
        {
            var repo = TagFactory.CreateTagRepository();

            repo.RemoveTag(tagToRemove);
        }
    }
}

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

        public void AddTag(Tag tagToAdd, int blogId)
        {
            var repo = TagFactory.CreateTagRepository();

            repo.AddTag(tagToAdd, blogId);
        }

        public void RemoveTag(Tag tagToRemove)
        {
            var repo = TagFactory.CreateTagRepository();

            repo.RemoveTag(tagToRemove);
        }

        public List<Tag> GetTopTenTags()
        {
            var repo = TagFactory.CreateTagRepository();

            var tenTags = repo.GetAllTags().Take(10);

            return tenTags.ToList();

            //This is the test method!!! not the PRod method will 
            //write when DB is finished... need the SQL List

        } 



        //Met and create tags

        public List<Tag> addTagToPost(string taglist, int BlogId)
        {
            var tags = new List<Tag>();
            var tagWords = taglist.Split(',');

            foreach (var name in tagWords)
            {
                tags.Add(doestagexist(name, BlogId));

            }
            return tags;

        }

        public Tag doestagexist(string tagname, int blogId)
        {
            var repo = TagFactory.CreateTagRepository();
            var tagtoreturn = new Tag();
            var oldTags = repo.GetAllTags();

            var isTagThere = oldTags.Exists(t => t.Name == tagname);

          
                if (isTagThere == true)
                {
                var tag = oldTags.FirstOrDefault(t => t.Name == tagname);
                tagtoreturn = tag;
                }

                else //if(isTagThere ==  false)
                {
                    var newtag = new Tag();
                    newtag.Name = tagname;
                    repo.AddTag(newtag, blogId);
               
                    tagtoreturn = newtag;
                }
                
            return tagtoreturn;
        }
    }
}

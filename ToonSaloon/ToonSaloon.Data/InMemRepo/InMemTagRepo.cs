using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.Interface;
using ToonSaloon.Models;

namespace ToonSaloon.Data.InMemRepo
{
    public class InMemTagRepo : ITagRepository
    {
        private static List<Tag> _tags;

        public InMemTagRepo()
        {
            if (_tags == null)
            {
                _tags = new List<Tag>
                {
                    new Tag()
                    {
                        Id = 1,
                        Name = "Rick and Morty"
                    },
                    new Tag()
                    {
                        Id = 3,
                        Name = "Cosmos"
                    },
                    new Tag()
                    {
                        Id = 2,
                        Name = "Static Page"
                    },new Tag()
                    {
                        Id = 2,
                        Name = "Static Page"
                    },
                    new Tag()
                    {
                        Id = 2,
                        Name = "Static Page"
                    },
                    new Tag()
                    {
                        Id = 2,
                        Name = "Static Page"
                    },
                    new Tag()
                    {
                        Id = 2,
                        Name = "Static Page"
                    },new Tag()
                    {
                        Id = 2,
                        Name = "Static Page"
                    },
                    new Tag()
                    {
                        Id = 2,
                        Name = "Static Page"
                    },
                    new Tag()
                    {
                        Id = 2,
                        Name = "Static Page"
                    },
                    new Tag()
                    {
                        Id = 2,
                        Name = "Static Page"
                    }
                };
            }
        }

        public List<Tag> GetAllTags()
        {
           return _tags;
        }

        public Tag GetTagById(int id)
        {
            return _tags.FirstOrDefault(t => t.Id == id);
        }

        public void AddTag(Tag tagToAdd, int blogId)
        {
            _tags.Add(tagToAdd);
        }

        public void RemoveTag(Tag tagToRemove)
        {
            var result = _tags.FirstOrDefault(t => t.Id == tagToRemove.Id);
            _tags.Remove(result);
        }

        //public List<Tag> addTagToPost(string taglist)
        //{
        //    var tags = new List<Tag>();
        //    var tagWords = taglist.Split(',');

        //    //var oldTags = GetAllTags();
            

        //    foreach (var name in tagWords)
        //    {
        //        tags.Add(doestagexist(name));
                
        //    }
        //    return tags;
            
        //}

        //public Tag doestagexist(string tagname)
        //{

        //    var tagtoreturn = new Tag();
        //    var oldTags = GetAllTags();

        //    foreach (var tag in oldTags)
        //    {

        //        if (tagname == tag.Name)
        //        {

        //            tagtoreturn = tag; 
        //        }
        //        else //if (tagname != tag.Name)
        //        {
        //            var newtag = new Tag();
        //            newtag.Name = tagname;
        //            newtag.Id = oldTags.Max(t => t.Id) + 1;
        //            AddTag(newtag);

        //            tagtoreturn =  newtag;
        //        }
        //        break;
               
        //    }
        //    return tagtoreturn;
        //}
    }
}

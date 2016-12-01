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

        public void AddTag(Tag tagToAdd)
        {
            _tags.Add(tagToAdd);
        }

        public void RemoveTag(Tag tagToRemove)
        {
            var result = _tags.FirstOrDefault(t => t.Id == tagToRemove.Id);
            _tags.Remove(result);
        }
    }
}

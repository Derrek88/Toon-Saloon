using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Models;

namespace ToonSaloon.Data.Interface
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();

        Tag GetTagById(int id);

        void AddTag(Tag tagToAdd);

        void RemoveTag(Tag tagToRemove);
    }
}

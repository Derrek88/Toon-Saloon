using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ToonSaloon.Models.Models
{
    public class StaticPageSearchVM
    {
        public List<SelectListItem> Tags { get; set; }
        public List<int> SelectedTagIds { get; set; }
        public StaticPage Page { get; set; }

        public StaticPageSearchVM()
        {
            // Categories = new List<SelectListItem>();
            Tags = new List<SelectListItem>();
            Page = new StaticPage();
            SelectedTagIds = new List<int>();
        }

        public void SetTags(IEnumerable<Tag> tags)
        {
            foreach(var tag in tags)
            {
                Tags.Add(new SelectListItem()
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Name
                });
            }
        }
    }
}

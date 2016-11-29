using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToonSaloon.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public List<string> Tags { get; set; }
        public string AuthorName { get; set; }
        public Enum Category { get; set; }
    }
}

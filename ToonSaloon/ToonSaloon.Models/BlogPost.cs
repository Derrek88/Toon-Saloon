using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToonSaloon.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "What is this post about?")]
        public string Body { get; set; }
       
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "What tags does this post need?")]
        public List<Tag> Tags { get; set; }

        [Required(ErrorMessage = "Who is posting this?")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "What Category does the post belong in?")]
        public Enum Category { get; set; }

        public bool isApproved { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ToonSaloon.Models
{
    public class StaticPage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "What is the name?")]
        public string Name { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "What is is made up of?")]
        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        public Category Category { get; set; }

        public Approved Approved { get; set; }

        public List<Tag> Tag { get; set; }

        // does not need stored
        public List<BlogPost> Posts { get; set; }

        
    }
}

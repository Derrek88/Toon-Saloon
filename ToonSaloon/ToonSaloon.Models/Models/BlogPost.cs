using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ToonSaloon.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "What is this post about?")]
        public string Body { get; set; }

        public string Headline { get; set; }

        public string Subtitle { get; set; }
       
        public DateTime DateCreated { get; set; }

        public List<Tag> Tags { get; set; }

        [Required(ErrorMessage = "Who is posting this?")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "What Category does the post belong in?")]
        public Category Category { get; set; }

        public Approved Approved { get; set; }

        public List<Img> Imgs { get; set; }

        //just a placeholder for tags don't think this needs to go in the database

        [Required(ErrorMessage = "What tags does this post need?")]
        public string TagPlaceHolder { get; set; }

    }
}

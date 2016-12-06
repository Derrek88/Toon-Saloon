using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToonSaloon.Models
{
    public class CartoonOfTheDay
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Who made this post?")]
        public string Author { get; set; }

        public Approved Approved { get; set; }

        public string ImgUrl { get; set; }

        //newbool! I iz saries
        public bool HasNotBeenPosted { get; set; }

        public DateTime WhenPosted { get; set; }

        [Required(ErrorMessage = "Whats the shows name?")]
        public string ShowName { get; set; }

        [Required(ErrorMessage = "What season is the episode in?")]
        public int Season { get; set; }

        [Required(ErrorMessage = "What episode is it?")]
        public int Episode { get; set; }

        public DateTime DateCreated { get; set; }
    }

   
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToonSaloon.Models
{
   public class Img 
   {
        public int Id { get; set; }

        [Required(ErrorMessage = "What is the title?")]
        public string Title { get; set; }

        [Required(ErrorMessage = "What is the source?")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Tell us about this image")]
        public string Description { get; set; }
      
   }
}

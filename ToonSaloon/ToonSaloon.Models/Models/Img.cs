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

        public string Title { get; set; }

        public string Source { get; set; }

        public string Description { get; set; }
      
   }
}

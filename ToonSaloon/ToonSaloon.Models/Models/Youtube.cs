using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToonSaloon.Models
{
    public class Youtube
    {
       [Required(ErrorMessage = "What is its 11 character code?")]
        public string TubeId { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Tell us about this video")]
        public string Description { get; set; }
    }
}

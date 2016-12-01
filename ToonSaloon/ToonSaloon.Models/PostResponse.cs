using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToonSaloon.Models
{
    public class PostResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public BlogPost BlogPost { get; set; }
    }
}

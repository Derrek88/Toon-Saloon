using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToonSaloon.Models.Responses
{
   public class StaticResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public StaticPage StaticPage { get; set; }
    }
}

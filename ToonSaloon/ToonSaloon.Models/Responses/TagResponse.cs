using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToonSaloon.Models.Responses
{
    public class TagResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public Tag Tag { get; set; }
    }
}

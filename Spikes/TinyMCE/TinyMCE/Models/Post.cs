using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TinyMCE.Models
{
    public class BlogPost
    {
        public int PostId { get; set; }

        [AllowHtml]
        public string PostBody { get; set; }
    }
}
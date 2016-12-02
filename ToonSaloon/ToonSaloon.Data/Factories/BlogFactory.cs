using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.InMemRepo;

namespace ToonSaloon.Data.Factories
{
    public class BlogFactory
    {

        public static IBlogPostRepository CreatBlogPostRepository()
        {
            IBlogPostRepository repo;

            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode)
            {
                case "Test":
                    repo = new InMemBlogRepo();
                    break;
                case "Prod":
                    repo = new BlogDBRepo();
                    break;
                default:
                    throw new Exception($"{mode} is not a recognized configuration");
            }
            return repo;
        }

    }
}

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
        //   public class IBlogPostRepository CreateBlogPostRepository()
        //   {
        //       InMemBlogRepo repo;

        //       string mode = ConfigurationManager.AppSettings["Mode"].ToString();
        //       switch (mode)
        //       {
        //            case "Test":
        //                repo = new InMemBlogRepo();
        //               break;
        //            case "Prod":
        //                throw new NotImplementedException();
        //            default:
        //                throw new Exception($"{mode} is not a recognized configuration");
        //       }
        //       return repo;
        //   }
        //}
            public static IBlogPostRepository CreatBlogPostRepository()
            {
                IBlogPostRepository repo;

                string mode = ConfigurationManager.AppSettings["mode"].ToString();
                switch (mode)
                {
                    case "test":
                        repo = new InMemBlogRepo();
                        break;
                    default:
                        throw new Exception($"{mode} is not a recognized configuration");
                }
                return repo;
            }
        
    }
}

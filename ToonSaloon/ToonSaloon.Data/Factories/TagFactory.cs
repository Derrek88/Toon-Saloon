using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.InMemRepo;
using ToonSaloon.Data.Interface;

namespace ToonSaloon.Data.Factories
{
    public class TagFactory
    {
        public static ITagRepository CreateTagRepository()
        {
            ITagRepository repo;

            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode)
            {
                case "Test":
                    repo = new InMemTagRepo();
                    break;
                case "Prod":
                    throw new NotImplementedException();
                default:
                    throw new Exception($"{mode} is not a recognized configuration");
            }
            return repo;
        }
    }
}

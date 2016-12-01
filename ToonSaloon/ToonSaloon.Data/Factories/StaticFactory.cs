using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.InMemRepo;
using ToonSaloon.Data.Interface;
using ToonSaloon.Models;

namespace ToonSaloon.Data.Factories
{
    public class StaticFactory
    {
        public static IStaticRepository CreateStaticPageRepository()
        {
            IStaticRepository repo;

            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode)
            {
                case "Test":
                    repo = new InMemStaticRepo();
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

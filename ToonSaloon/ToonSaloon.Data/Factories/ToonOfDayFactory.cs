using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.DBRepos;
using ToonSaloon.Data.InMemRepo;
using ToonSaloon.Data.Interface;

namespace ToonSaloon.Data.Factories
{
    public class ToonOfDayFactory
    {
        public static IToonOfDayRepository CreateToonOfDayRepository()
        {
            IToonOfDayRepository repo;

            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode)
            {
                case "Test":
                    repo = new InMemToonOfDayRepo();
                    break;
                case "Prod":
                     repo = new ToonOfDayDBRepo();
                    break;
                default:
                    throw new Exception($"{mode} is not a recognized configuration");
            }
            return repo;
        }
    }
}

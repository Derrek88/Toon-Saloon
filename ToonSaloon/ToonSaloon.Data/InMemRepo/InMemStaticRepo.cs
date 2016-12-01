using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.Interface;
using ToonSaloon.Models;

namespace ToonSaloon.Data.InMemRepo
{
    public class InMemStaticRepo : IStaticRepository
    {
        private static List<StaticPage> _pages;

        public InMemStaticRepo()
        {
            if (_pages == null)
            {
                _pages = new List<StaticPage>()
                {
                    new StaticPage()
                    {
                        Id = 1,
                        Name = "About Us",
                        Body = "Welcome to ToonSaloon! All we want to do is expose our favorite cartoons too the public so they gain even more popularity!",
                        DateCreated = DateTime.Today,
                        Approved = Approved.Yes,
                        Tag = new List<Tag>
                        {
                            new Tag()
                            {
                                Id = 1,
                                Name = "Static Page"
                            }
                        }
                    },
                    new StaticPage()
                    {
                        Id = 2,
                        Name = "Contact Us",
                        Body = "randomemail@randomsite.com",
                        DateCreated = DateTime.Today,
                        Approved = Approved.Yes,
                        Tag = new List<Tag>
                        {
                            new Tag()
                            {
                                Id = 2,
                                Name = "Static Page"
                            }
                        }
                    }
                };
            }
        }

        public StaticPage GetPageByID(int id)
        {
            return _pages.FirstOrDefault(p => p.Id == id);
        }

        public List<StaticPage> GetAllPages()
        {
            return _pages;
        }

        public void AddStaticPage(StaticPage pageToAdd)
        {
            _pages.Add(pageToAdd);
        }

        public void RemoveStaticPage(StaticPage pageToRemove)
        {
            var result = _pages.FirstOrDefault(p => p.Id == pageToRemove.Id);
            _pages.Remove(result);
        }

        public void EditStaticPage(StaticPage pageToEdit)
        {
            var page = _pages.FirstOrDefault(p => p.Id == pageToEdit.Id);
            _pages.Remove(page);
            page = pageToEdit;
            _pages.Add(page);
        }
    }
}

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
                    },
                    new StaticPage()
                    {
                        Id = 3 ,
                        Name = "Policies and Regulations",
                        Body = @"This policy provides guidance for employee use of social media, which should be broadly
understood for purposes of this policy to include blogs, wikis, microblogs, message boards, chat
rooms, electronic newsletters, online forums, social networking sites, and other sites and services
that permit users to share information with others in a contemporaneous manner.
PROCEDURES
The following principles apply to professional use of social media on behalf of[Company] as well as
personal use of social media when referencing[Company].
 Employees need to know and adhere to the[Company’s Code of Conduct, Employee
Handbook, and other company policies] when using social media in reference to[Company].
 Employees should be aware of the effect their actions may have on their images, as well as
[Company’s] image.The information that employees post or publish may be public information
for a long time.
 Employees should be aware that[Company] may observe content and information made
available by employees through social media.Employees should use their best judgment in
posting material that is neither inappropriate nor harmful to[Company], its employees, or
customers.
 Although not an exclusive list, some specific examples of prohibited social media conduct
include posting commentary, content, or images that are defamatory, pornographic, proprietary,
harassing, libelous, or that can create a hostile work environment.
 Employees are not to publish, post or release any information that is considered confidential or
not public. If there are questions about what is considered confidential, employees should
check with the Human Resources Department and/or supervisor.
 Social media networks, blogs and other types of online content sometimes generate press and
media attention or legal questions. Employees should refer these inquiries to authorized
[Company] spokespersons.
 If employees find encounter a situation while using social media that threatens to become
antagonistic, employees should disengage from the dialogue in a polite manner and seek the
advice of a supervisor.
 Employees should get appropriate permission before you refer to or post images of current or
former employees, members, vendors or suppliers.Additionally, employees should get
appropriate permission to use a third party's copyrights, copyrighted material, trademarks,
service marks or other intellectual property. 
 Social media use shouldn't interfere with employee’s responsibilities at [Company]. [Company’s]
computer systems are to be used for business purposes only.When using [Company’s]
    computer systems, use of social media for business purposes is allowed(ex: Facebook, Twitter,
    [Company] blogs and LinkedIn), but personal use of social media networks or personal
    blogging of online content is discouraged and could result in disciplinary action.
     Subject to applicable law, after‐hours online activity that violates[the Company’s Code of
    Conduct] or any other company policy may subject an employee to disciplinary action or
    termination.
     If employees publish content after‐hours that involves work or subjects associated with
    [Company], a disclaimer should be used, such as this: “The postings on this site are my own
    and may not represent [Company’s] positions, strategies or opinions.”
 It is highly recommended that employees keep[Company] related social media accounts
    separate from personal accounts, if practical.",

                            DateCreated = DateTime.Today,
                        Approved = Approved.Yes,
                        Tag = new List<Tag>
                        {
                            new Tag()
                            {
                                Id = 3,
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

        public void EditTagStaticBridgeTable(StaticPage id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTagStaticBridgeTable(StaticPage id)
        {
            throw new NotImplementedException();
        }

        public void InsertTagStaticBridgeTable(int tagId, int pageId)
        {
            throw new NotImplementedException();
        }

        public void InsertTagStaticBridgeTable(StaticPage id)
        {
            throw new NotImplementedException();
        }
    }
}

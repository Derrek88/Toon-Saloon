using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Models;

namespace ToonSaloon.Data.InMemRepo
{
    public class InMemBlogRepo : IBlogPostRepository
    {
        private static List<BlogPost> _posts;

        public InMemBlogRepo()
        {
            if (_posts == null)
            {
                _posts = new List<BlogPost>() 
                {
                    new BlogPost()
                    {
                        Id = 1,
                        Body = "Hydrogen atoms, shores of the cosmic ocean great turbulent clouds, from which we spring science hundreds of thousands. Hearts of the stars decipherment extraplanetary Orion\'s sword, a billion trillion network of wormholes circumnavigated. Made in the interiors of collapsing stars descended from astronomers Jean-François Champollion brain is the seed of intelligence, consciousness, birth. Citizens of distant epochs Sea of Tranquility Rig Veda. A mote of dust suspended in a sunbeam Drake Equation intelligent beings ship of the imagination rings of Uranus rogue Euclid. Preserve and cherish that pale blue dot.\r\nHundreds of thousands, billions upon billions. Two ghostly white figures in coveralls and helmets are soflty dancing network of wormholes Apollonius of Perga billions upon billions, kindling the energy hidden in matter, rings of Uranus, tendrils of gossamer clouds? Shores of the cosmic ocean colonies circumnavigated explorations star stuff harvesting star light corpus callosum hundreds of thousands rich in heavy atoms colonies white dwarf. Gathered by gravity another world how far away rich in heavy atoms rich in mystery, encyclopaedia galactica colonies kindling the energy hidden in matter colonies.",
                        AuthorName = "Jacob",
                        Headline = "Carl Sagan reveals the truth!",
                        Subtitle = "annnnd no one listened... well not every one",
                        Tags = new List<Tag>
                        {
                           new Tag()
                           {
                               Id = 1,
                               Name = "Rick and Morty"
                           },
                           new Tag()
                           {
                               Id = 2,
                               Name = "Cosmos"
                           },
                           new Tag()
                           {
                               Id = 3,
                               Name = "Science"
                           },
                           new Tag()
                           {
                               Id = 4,
                               Name = "Wub-A-Lub-A-Dub-Dub"
                           },
                           new Tag()
                           {
                               Id = 5,
                               Name = "Gilb-Glob"
                           },
                        },
                        Category = Category.Childrens,
                        Imgs = new List<Img>
                        {
                            new Img()
                            {
                                Id = 1,
                                Title = "Random Image",
                                Source = "../../Images/appimages/rick.jpg",
                                Description = "a watch"
                            },
                            new Img()
                            {
                                Id = 2,
                                Title = "Random Image",
                                Source = "../../Images/appimages/rick.jpg",
                                Description = "a watch"
                            },
                            new Img()
                            {
                                Id = 3,
                                Title = "Random Image",
                                Source = "../../Images/appimages/rick.jpg",
                                Description = "a watch"
                            }
                        },
                        Approved = Approved.Waiting,
                        DateCreated = Convert.ToDateTime("11/28/2016"),
                        
                        
                    },
                    new BlogPost()
                    {
                        Id = 2,
                        Body = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Curabitur sodales ligula in libero. 

                                 Sed dignissim lacinia nunc. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit.",
                        AuthorName = "Jacob",
                        Headline = "Will rick escape?!?1",
                        Subtitle = "Well, there has to be another season right?",
                        Tags = new List<Tag>
                        {
                           new Tag()
                           {
                               Id = 6,
                               Name = "Rick and Morty"
                           },
                           new Tag()
                           {
                               Id = 7,
                               Name = "Doc"
                           },
                           new Tag()
                           {
                               Id = 8,
                               Name = "Back to the Future"
                           },
                           new Tag()
                           {
                               Id = 9,
                               Name = "glub glub"
                           },
                           new Tag()
                           {
                               Id = 10,
                               Name = "zipppydoo"
                           },





                        },
                        Category = Category.Childrens,
                        Imgs = new List<Img>
                        {
                            new Img()
                            {
                                Id = 1,
                                Title = "Random Image",
                                Source = "../../Images/appimages/art-false-positive.jpg",
                                Description = "a watch"
                            }
                        },
                        Approved = Approved.Yes,
                        DateCreated = Convert.ToDateTime("11/25/2016")
                    },
                };
            }
        }


        public BlogPost GetPostByID(int id)
        {
            return _posts.FirstOrDefault(p => p.Id == id);
        }

        public List<BlogPost> GetAllPosts()
        {
            return _posts;
        }

        public void AddBlogPost(BlogPost postToAdd)

        {

            
            //var taglist = new List<Tag>();
            //var posttags = postToAdd.TagPlaceHolder;
            //var repo = new InMemTagRepo();
            //taglist = repo.addTagToPost(posttags);
            //postToAdd.Tags = taglist;
            //postToAdd.Id = GetAllPosts().Max(p => p.Id) + 1;
            //postToAdd.DateCreated = DateTime.Now;
            
            _posts.Add(postToAdd);
        }

        public void RemoveBlogPost(BlogPost postToRemove)
        {
            var result = _posts.FirstOrDefault(p => p.Id == postToRemove.Id);
            _posts.Remove(result);
        }

        public void EditBlogPost(BlogPost postToEdit)
        {
            var post = _posts.FirstOrDefault(p => p.Id == postToEdit.Id);
            _posts.Remove(post); 
            _posts.Add(postToEdit);
        }

        public List<BlogPost> GetPostByTag(string TagName)
        {
            throw new NotImplementedException();
            //var tagPosts = _posts.Where(p => p.Tags.);
            //tagPosts = tagPosts.Where
        }

        public void AddImageToBlogPost(Img imgToAdd, int blogid)
        {
            throw new NotImplementedException();
        }

        public void InsertImgBlogBridgeTable(int imgId, int newBlogId)
        {
            throw new NotImplementedException();
        }

        public void RemoveImageToBlogPost(Img imgToDelete)
        {
            throw new NotImplementedException();
        }

        public void DeleteImgBlogBridgeTable(BlogPost id)
        {
            throw new NotImplementedException();
        }

        public void EditImageOnBlogPost(Img imgToEdit)
        {
            throw new NotImplementedException();
        }

        public void EditImgBlogBridgeTable(BlogPost id)
        {
            throw new NotImplementedException();
        }

        public void AddTagIntoBlogPost(Tag tagToAdd)
        {
            throw new NotImplementedException();
        }

        public void EditTagFromBlogPost(Tag tagToEdit)
        {
            throw new NotImplementedException();
        }

        public void DeleteTagFromBlogPost(Tag tagToDelete)
        {
            throw new NotImplementedException();
        }

        public void InsertTagBlogBridgeTable(int tagId, int newBlogId)
        {
            throw new NotImplementedException();
        }

        public void EditTagBlogBridgeTable(BlogPost id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTagBlogBridgeTable(BlogPost id)
        {
            throw new NotImplementedException();
        }
    }
}

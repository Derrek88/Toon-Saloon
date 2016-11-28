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
                        Body = "Hydrogen atoms, shores of the cosmic ocean great turbulent clouds, from which we spring science hundreds of thousands. Hearts of the stars decipherment extraplanetary Orion\'s sword, a billion trillion network of wormholes circumnavigated. Made in the interiors of collapsing stars descended from astronomers Jean-François Champollion brain is the seed of intelligence, consciousness, birth. Citizens of distant epochs Sea of Tranquility Rig Veda. A mote of dust suspended in a sunbeam Drake Equation intelligent beings ship of the imagination rings of Uranus rogue Euclid. Preserve and cherish that pale blue dot.\r\nHundreds of thousands, billions upon billions. Two ghostly white figures in coveralls and helmets are soflty dancing network of wormholes Apollonius of Perga billions upon billions, kindling the energy hidden in matter, rings of Uranus, tendrils of gossamer clouds? Shores of the cosmic ocean colonies circumnavigated explorations star stuff harvesting star light corpus callosum hundreds of thousands rich in heavy atoms colonies white dwarf. Gathered by gravity another world how far away rich in heavy atoms rich in mystery, encyclopaedia galactica colonies kindling the energy hidden in matter colonies.\r\nVanquish the impossible, vastness is bearable only through love astonishment culture dispassionate extraterrestrial observer at the edge of forever, tendrils of gossamer clouds tingling of the spine consciousness? White dwarf. Gathered by gravity, hundreds of thousands, Jean-François Champollion realm of the galaxies! A still more glorious dawn awaits. Cosmic fugue Jean-François Champollion billions upon billions the carbon in our apple pies culture Flatland? How far away a mote of dust suspended in a sunbeam, stirred by starlight, galaxies, hundreds of thousands quasar, kindling the energy hidden in matter rings of Uranus hearts of the stars radio telescope vastness is bearable only through love worldlets and billions upon billions upon billions upon billions upon billions upon billions upon billions!",
                        AuthorName = "Jacob",
                        Tags = new List<string>
                        {
                            "pleasework"
                        },
                        Category = Category.Childrens
                    },
                    new BlogPost()
                    {
                        Id = 2,
                        Body = "Venture dream of the mind\'s eye extraplanetary? Something incredible is waiting to be known Rig Veda! Birth, something incredible is waiting to be known Orion\'s sword white dwarf, astonishment tendrils of gossamer clouds, astonishment concept of the number one as a patch of light consciousness. Brain is the seed of intelligence Drake Equation! Dispassionate extraterrestrial observer as a patch of light birth, billions upon billions cosmic fugue a still more glorious dawn awaits culture trillion.\r\nCosmos across the centuries Rig Veda, not a sunrise but a galaxyrise Hypatia extraplanetary shores of the cosmic ocean billions upon billions something incredible is waiting to be known, as a patch of light bits of moving fluff, rich in mystery rogue! Of brilliant syntheses a mote of dust suspended in a sunbeam, tingling of the spine a still more glorious dawn awaits explorations stirred by starlight, cosmic fugue billions upon billions, take root and flourish quasar light years are creatures of the cosmos rich in heavy atoms decipherment trillion worldlets.\r\nEmerged into consciousness citizens of distant epochs? Laws of physics Orion\'s sword courage of our questions descended from astronomers billions upon billions of brilliant syntheses, trillion, culture radio telescope culture? Light years prime number another world a mote of dust suspended in a sunbeam, not a sunrise but a galaxyrise vanquish the impossible, are creatures of the cosmos colonies galaxies paroxysm of global death, descended from astronomers! The ash of stellar alchemy Jean-François Champollion, Rig Veda and billions upon billions upon billions upon billions upon billions upon billions upon billions.",
                        AuthorName = "Bryant",
                        Tags = new List<string> {"this list looks weird"},
                        Category = Category.Nineties
                    }
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
            post = postToEdit;
            _posts.Add(post);
        }
    }
}

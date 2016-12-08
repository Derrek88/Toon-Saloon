using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.BLL;
using ToonSaloon.Data.DBRepos;
using ToonSaloon.Data.Factories;
using ToonSaloon.Models;
using ToonSaloon.Web.Controllers;


namespace ToonSaloon.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void addPostCheckThenDelete()
        {
            var repo = BlogFactory.CreatBlogPostRepository();
            var manager = new PostManager();

            BlogPost testPost = new BlogPost();
            var filename = "test.png";
            var imgs = new List<Img>();
            var tags = new List<Tag>();

            testPost.Headline = "Testing";
            testPost.Subtitle = "Test Sub";
            testPost.Body = "Testing again";
            testPost.AuthorName = "Test";
            testPost.Approved = Approved.Yes;
            testPost.Category = Category.Anime;
            testPost.DateCreated = DateTime.Today;
            testPost.TagPlaceHolder = "Test1,Test2";
            var img = new Img()
            {
                Description = "Test description",
                Title = "Test title",
                Source = filename
            };
            imgs.Add(img);
            testPost.Imgs = imgs;

            Tag tag1 = new Tag()
            {
                Name = "Test1"
            };
            tags.Add(tag1);
            Tag tag2 = new Tag()
            {
                Name = "Test2"
            };
            tags.Add(tag2);

            testPost.Tags = tags;

            repo.AddBlogPost(testPost);
            List<BlogPost> posts = repo.GetAllPosts();

            BlogPost check = posts.Last();
            Assert.AreEqual("Test Sub", check.Subtitle);
            Assert.AreEqual("Testing", check.Headline);
            Assert.AreEqual("Testing again", check.Body);
            Assert.AreEqual("Testing", check.Headline);
            //Assert.AreEqual("Test1", check.Tags[0].Name);
            //Assert.AreEqual("Test2", check.Tags[1].Name);
            //Assert.AreEqual("Test Title", check.Imgs[0].Title);
            //Assert.AreEqual("Test description", check.Imgs[0].Description);

            repo.RemoveBlogPost(testPost);

            Assert.IsNull(repo.GetPostByID(testPost.Id));
        }


        //Test is good
        [Test]
        public void addToonCheckTheDelete()
        {

            var repo = ToonOfDayFactory.CreateToonOfDayRepository();

            CartoonOfTheDay toon = new CartoonOfTheDay();
            var filename = "img.png";

            toon.ShowName = "That show";
            toon.Season = 1;
            toon.Episode = 1;
            toon.DateCreated = DateTime.Today;
            toon.Author = "Derrek";
            toon.Approved = Approved.Yes;
            toon.ImgUrl = "" + filename;
            toon.WhenPosted = DateTime.Today;
            toon.HasNotBeenPosted = false;

            repo.AddToonOfDay(toon);

            List<CartoonOfTheDay> toons = repo.GetAllToons();


            CartoonOfTheDay check = toons.Last();
            Assert.AreEqual("Derrek", check.Author);
            Assert.AreEqual("That show", check.ShowName);

            repo.RemoveToonOfDay(toon);

            // check that this toon is no longer in database
            Assert.IsNull(repo.GetPostByID(toon.Id));
        }

        [Test]
        public void addStaticPageCheckTheDelete()
        {
            var repo = StaticFactory.CreateStaticPageRepository();

            StaticPage page = new StaticPage();

            page.Name = "Anime";
            page.Body = "Anime stuff";
            page.Category = Category.Anime;
            page.Approved = Approved.Yes;
            page.DateCreated = DateTime.Today;

            repo.AddStaticPage(page);
            List<StaticPage> pages = repo.GetAllPages();
            StaticPage check = pages.Last();

            Assert.AreEqual("Anime", check.Name);
            Assert.AreEqual("Anime stuff", check.Body);
            //Assert.AreEqual(Anime, check.Category);
            //Assert.AreEqual("Yes", check.Approved);

            repo.RemoveStaticPage(page);

            Assert.IsNull(repo.GetPageByID(page.Id));

        }
    }
}

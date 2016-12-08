using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Data.Factories;
using ToonSaloon.Data.Interface;
using ToonSaloon.Models;
using ToonSaloon.Models.Responses;

namespace ToonSaloon.BLL
{
    public class StaticManger 
    {
        public StaticPage GetPostByID(int id)
        {
            StaticResponse response = new StaticResponse();
            var repo = StaticFactory.CreateStaticPageRepository();
            var page = repo.GetPageByID(id);

            if (page != null)
            {
                response.Success = true;
                response.Message = "It worked!";
                response.StaticPage = page;
            }
            else
            {
                response.Success = false;
                response.Message = "Post not found!";
            }
            return page;
        }

        public List<StaticPage> GetAllPages()
        {
            var repo = StaticFactory.CreateStaticPageRepository();
            var page = repo.GetAllPages();
            return page;
        }

        public void AddStaticPage(StaticPage pageToAdd)
        {
            var repo = StaticFactory.CreateStaticPageRepository();

            repo.AddStaticPage(pageToAdd);

            foreach (var tag in pageToAdd.Tag)
            {
              InsertTagStaticBridgeTable(tag.Id, pageToAdd.Id);
            }
            
            
        }

        public void RemoveStaticPage(StaticPage pageToRemove)
        {
            var repo = StaticFactory.CreateStaticPageRepository();

            repo.DeleteTagStaticBridgeTable(pageToRemove);
            repo.RemoveStaticPage(pageToRemove);
        }

        public void EditStaticPage(StaticPage pageToEdit)
        {
            var repo = StaticFactory.CreateStaticPageRepository();
            repo.DeleteTagStaticBridgeTable(pageToEdit);

            foreach (var tag in pageToEdit.Tag)
            {
                repo.InsertTagStaticBridgeTable(tag.Id, pageToEdit.Id);
            }

            repo.EditStaticPage(pageToEdit);

        }

        public void InsertTagStaticBridgeTable(int tagId, int pageId)
        {
            var repo = StaticFactory.CreateStaticPageRepository();

            repo.InsertTagStaticBridgeTable(tagId, pageId);
        }

        public StaticPage GetBySearch(int id)
        {
            var filteredPosts = new List<BlogPost>();
            var pageToReturn = new StaticPage();
            var categoryPosts = new List<BlogPost>();
            var tagposts = new List<BlogPost>();
            
            // get static page data
            var page = GetPostByID(id);             
            
            // get each post by Category
            if(page.Category != Category.None)
            {
                categoryPosts = GetCategoryPosts(page);
            }
            else
            {
                categoryPosts = null;
            }
          
            //get posts by tags
            if(page.Tag.Count > 0)
            {
                tagposts = GetTagPosts(page);
            }else
            {
                tagposts = null;
            }

            // get filtered tags if needed
            if(categoryPosts != null && tagposts != null)
            {
                var filter =  tagposts.Where(p => p.Category == page.Category).ToList();

                //filteredPosts = filter.GroupBy(i => i.Id)..ToList();
                filteredPosts = filter.Distinct().ToList();

                //  FilteredPosts(tagposts, categoryPosts);

            }
            else if(categoryPosts != null && tagposts == null)
            {
                filteredPosts = categoryPosts;
            }else if(categoryPosts == null && tagposts != null)
            {
                filteredPosts = tagposts;
            }else
            {
                filteredPosts = null;
            }

            pageToReturn = page;
            pageToReturn.Posts = filteredPosts;
            return pageToReturn;
        }

        public List<BlogPost> GetCategoryPosts(StaticPage page)
        {
            var repo = BlogFactory.CreatBlogPostRepository();
            var categoryPosts = new List<BlogPost>();
            var posts = repo.GetAllPosts();

            foreach (var post in posts)
            {
                if (page.Category == post.Category)
                {
                    categoryPosts.Add(post);
                }
            }
            return categoryPosts;
        }

        public List<BlogPost> GetTagPosts(StaticPage page)
        {
            var repo = BlogFactory.CreatBlogPostRepository();
            var tagposts = new List<BlogPost>();
            var posts = repo.GetAllPosts();

            foreach (var tag in page.Tag)
            {
                foreach(var post in posts)
                {
                    foreach(var posttag in post.Tags)
                    if(tag.Name == posttag.Name)
                        {
                            var postbytag = post;
                            tagposts.Add(postbytag);
                        }
                    
                }
                
            }

            return tagposts;
        }
    }
}

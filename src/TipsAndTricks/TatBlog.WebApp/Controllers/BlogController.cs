using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.DTO;
using TatBlog.Services.Blogs;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController:Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 10)
        {
            //ViewBag.CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            var postQuery = new PostQuery()
            {
                PublishedOnly = true,

                Keyword = keyword
            };

            var postsList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            ViewBag.PostQuery = postQuery;

            return View(postsList);
        }

        public async Task<IActionResult> Category(
            string slug, 
            int pageNumber = 1)
        {
            var postQuery = new PostQuery()
            {
                CategorySlug = slug
            };

            var postsList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber);

            ViewBag.PostQuery = postQuery;

            return View(postsList);
        }

        public async Task<IActionResult> Author(
            string slug,
            int pageNumber = 1)
        {
            var postQuery = new PostQuery()
            {
                AuthorSlug = slug
            };

            var postsList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber);

            ViewBag.PostQuery = postQuery;

            return View(postsList);
        }

        public IActionResult About()
            => View();

        public IActionResult Contact()
           => View();

        public IActionResult Rss()
           => Content("Nội dung sẽ được cập nhật");
    }
}

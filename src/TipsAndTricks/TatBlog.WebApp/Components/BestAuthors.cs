using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class BestAuthors : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IAuthorRepository _authorRepository;

        public BestAuthors(IBlogRepository blogRepository, AuthorRepository authorRepository)
        {
            _blogRepository = blogRepository;
            _authorRepository = authorRepository;
        }
          
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _authorRepository.GetPopularAuthorAsync(4);
            return View(categories);
        }
    }
}

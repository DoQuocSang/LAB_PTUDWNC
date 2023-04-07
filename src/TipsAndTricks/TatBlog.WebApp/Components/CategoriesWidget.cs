using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class CategoriesWidget : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesWidget(IBlogRepository blogRepository, CategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }
          
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            return View(categories);
        }
    }
}

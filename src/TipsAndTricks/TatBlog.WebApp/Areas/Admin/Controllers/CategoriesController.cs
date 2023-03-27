using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;
using TatBlog.WebApp.Validations;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public CategoriesController(
           IBlogRepository blogRepository,
           IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(
           CategoryFilterModel model,
           int pageNumber = 1,
           int pageSize = 5)
        {
            //var postQuery = new PostQuery()
            //{
            //    Keyword = model.Keyword,
            //    CategoryId = model.CategoryId,
            //    AuthorId = model.AuthorId,
            //    Year = model.Year,
            //    Month = model.Month
            //};

            //var postQuery = _mapper.Map<PostQuery>(model);


            ViewBag.CategoriesList = await _blogRepository
                .GetPagedCategoriesAsync(pageNumber, pageSize);

            //await PopulatePostFilterModelAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id = 0)
        {
            var category = id > 0
                ? await _blogRepository.GetCategoryByIdAsync(id)
                : null;

            var model = category == null
                ? new CategoryEditModel()
                : _mapper.Map<CategoryEditModel>(category);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = model.Id > 0
                ? await _blogRepository.GetCategoryByIdAsync(model.Id) : null;

            if (category == null)
            {
                category = _mapper.Map<Category>(model);
                category.Id = 0;
            }
            else
            {
                _mapper.Map(model, category);
            }

            await _blogRepository.CreateOrUpdateCategoryAsync(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeCategoryShowOnMenuState(int id)
        {
            await _blogRepository.ToggleShowOnMenuFlagAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _blogRepository.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
